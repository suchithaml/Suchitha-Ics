using Newtonsoft.Json;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Http;
using System.Data.Entity.Core;
using E_Trading_WebAPI.Models;



namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/custorders")]
    public class CustomerOrdersController : ApiController
    {
        private readonly ETradingDBEntities _context;

        public CustomerOrdersController()
        {
            _context = new ETradingDBEntities(); // EF DbContext
        }

        // POST: Process a new order
        [HttpPost]
        [Route("process")]
        public IHttpActionResult ProcessOrder(OrderRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.OrderDetails == null || orderRequest.OrderDetails.Count == 0)
            {
                return BadRequest("Invalid order data.");
            }

            try
            {
                // Step 1: Check the customer's balance in the TopUp table
                var customerBalance = _context.TopUps
                    .Where(t => t.UserID == orderRequest.CustomerID)
                    .Select(t => t.Balance)
                    .FirstOrDefault();

                if (customerBalance == null)
                {
                    return NotFound(); // Customer not found in TopUp table
                }

                if (customerBalance < orderRequest.OrderTotal)
                {
                    return Ok(new { success = false, message = "Insufficient balance." });
                }

                // Step 2: Deduct balance from the customer's TopUp account
                var customer = _context.TopUps.FirstOrDefault(t => t.UserID == orderRequest.CustomerID);
                if (customer != null)
                {
                    customer.Balance -= orderRequest.OrderTotal;
                }

                // Step 3: Convert OrderDetails to JSON string
                string orderDetailsJson = JsonConvert.SerializeObject(orderRequest.OrderDetails);

                // Step 4: Execute the stored procedure using Entity Framework
                var orderIdParameter = new SqlParameter("@OrderID", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                var customerIdParameter = new SqlParameter("@CustomerID", orderRequest.CustomerID);
                var vendorIdParameter = new SqlParameter("@VendorID", orderRequest.VendorID);
                var orderTotalParameter = new SqlParameter("@OrderTotal", orderRequest.OrderTotal);
                var orderDetailsParameter = new SqlParameter("@OrderDetails", orderDetailsJson);

                // Execute the stored procedure
                _context.Database.ExecuteSqlCommand(
                    "EXEC ProcessOrder @CustomerID, @VendorID, @OrderTotal, @OrderDetails, @OrderID OUTPUT",
                    customerIdParameter, vendorIdParameter, orderTotalParameter, orderDetailsParameter, orderIdParameter
                );

                int orderId = (int)orderIdParameter.Value; // Retrieve the output parameter (OrderID)

                // Step 5: Return the success response with the generated OrderID
                return Ok(new { success = true, orderId });
            }
            catch (Exception ex)
            {
                // Log the error
                return InternalServerError(new Exception("An error occurred while processing the order.", ex));
            }
        }

        //// GET: api/orders/history/{customerId}
        //[HttpGet]
        //[Route("history/{customerId}")]
        //public IHttpActionResult GetOrderHistory(int customerId)
        //{
        //    try
        //    {
        //        // Check if the customer exists in the Users table
        //        var customerExists = _context.Users.Any(u => u.UserID == customerId);
        //        if (!customerExists)
        //        {
        //            return NotFound(); // Return 404 if the customer does not exist
        //        }

        //        // Use raw SQL query to fetch the order history
        //        var query = @"
        //    SELECT
        //        o.OrderID,
        //        o.OrderDate,
        //        o.OrderTotal,
        //        o.OrderStatus,
        //        v.Username AS VendorName,
        //        od.ProductID,
        //        p.ProductName,
        //        od.Quantity,
        //        od.TotalPrice
        //    FROM Orders o
        //    INNER JOIN Users v ON o.VendorID = v.UserID
        //    INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
        //    INNER JOIN Products p ON od.ProductID = p.ProductID
        //    WHERE o.CustomerID = @CustomerID
        //    ORDER BY o.OrderDate DESC";

        //        var result = _context.Database.SqlQuery<OrderHistoryViewModel>(
        //            query,
        //            new SqlParameter("@CustomerID", customerId)
        //        ).ToList();



        //        if (result == null || result.Count == 0)
        //        {
        //            return NotFound(); // Return 404 if no orders are found
        //        }

        //        return Ok(result); // Return the orders in the response
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (using a logging framework, if applicable)
        //        return InternalServerError(new Exception("An error occurred while retrieving the order history.", ex));
        //    }
        //}

        // GET: api/orders/history/{customerId}
        [HttpGet]
        [Route("history/{customerId}")]
        public IHttpActionResult GetOrderHistory(int customerId)
        {
            try
            {
                // Check if the customer exists in the Users table
                var customerExists = _context.Users.Any(u => u.UserID == customerId);
                if (!customerExists)
                {
                    return NotFound(); // Return 404 if the customer does not exist
                }

                // Use raw SQL query to fetch the order history
                var query = @"
        SELECT
            o.OrderID,
            o.OrderDate,
            o.OrderTotal,
            o.OrderStatus,
            v.Username AS VendorName,
            od.ProductID,
            p.ProductName,
            od.Quantity,
            od.TotalPrice
        FROM Orders o
        INNER JOIN Users v ON o.VendorID = v.UserID
        INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
        INNER JOIN Products p ON od.ProductID = p.ProductID
        WHERE o.CustomerID = @CustomerID
        ORDER BY o.OrderDate DESC";

                var rawData = _context.Database.SqlQuery<OrderHistoryRawData>(
                    query,
                    new SqlParameter("@CustomerID", customerId)
                ).ToList();

                if (rawData == null || rawData.Count == 0)
                {
                    return NotFound(); // Return 404 if no orders are found
                }

                // Group the raw data by OrderID and create the final view model
                var groupedResult = rawData
    .GroupBy(x => x.OrderID)
    .Select(g => new OrderHistoryViewModel
    {
        OrderID = g.Key,
        OrderDate = g.First().OrderDate,
        OrderTotal = g.First().OrderTotal,
        OrderStatus = g.First().OrderStatus,
        VendorName = g.First().VendorName,
        OrderDetails = g.Select(x => new MyOrderDetail
        {
            ProductID = x.ProductID,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            TotalPrice = x.TotalPrice
        }).ToList()
    }).ToList();

                return Ok(groupedResult); // Return the orders in the response
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework, if applicable)
                return InternalServerError(new Exception("An error occurred while retrieving the order history.", ex));
            }
        }

    }
}