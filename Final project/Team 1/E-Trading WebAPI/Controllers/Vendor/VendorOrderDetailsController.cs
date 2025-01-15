using System;
using System.Linq;
using System.Web.Http;

using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/orderdetails/vendor")]
    public class VendorOrderDetailsController : ApiController
    {
        
        private readonly ETradingDBEntities _context = new ETradingDBEntities();

        // Get order details by order ID
        [HttpGet]
        [Route("orders")]
        public IHttpActionResult GetAllOrdersWithDetails()
        {
            try
            {
                // Fetching orders along with their associated order details
                var ordersWithDetails = _context.Orders
                    .Join(_context.OrderDetails,
                        order => order.OrderID,
                        orderDetail => orderDetail.OrderID,
                        (order, orderDetail) => new
                        {
                            order.OrderID,
                            order.CustomerID,
                            order.VendorID,
                            order.OrderTotal,
                            order.OrderStatus,
                            order.OrderDate,
                            orderDetail.OrderDetailID,
                            orderDetail.ProductID,
                            orderDetail.Quantity,
                            orderDetail.TotalPrice
                        })
                    .ToList();

                if (ordersWithDetails.Count == 0)
                {
                    return NotFound();
                }

                return Ok(ordersWithDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Failed to retrieve orders with details.", ex));
            }
        }

        [HttpGet]
        [Route("{vendorId}")]
        public IHttpActionResult GetOrdersByVendorId(int vendorId)
        {
            try
            {
                // Fetch orders for the given vendorId from the Orders table
                var orders = _context.Orders
                                     .Where(o => o.VendorID == vendorId)
                                     .ToList();  // Execute the query and bring only the orders into memory

                // If no orders are found, return NotFound
                if (orders == null || orders.Count == 0)
                    return NotFound();

                // Get the order IDs for the orders belonging to the given vendor
                var orderIds = orders.Select(o => o.OrderID).ToList();  // Get only OrderIDs to use in the next query

                // Now filter OrderDetails using the list of OrderIDs
                var orderDetails = _context.OrderDetails
                                           .Where(od => orderIds.Contains(od.OrderID))  // Use the OrderID list in the query
                                           .ToList();

                // Combine Orders and OrderDetails into a list of OrderwithDetails using LINQ join
                var orderWithDetailsList = (from order in orders
                                            join detail in orderDetails on order.OrderID equals detail.OrderID
                                            select new OrderwithDetails
                                            {
                                                OrderID = order.OrderID,
                                                CustomerID = order.CustomerID,
                                                VendorID = order.VendorID,
                                                OrderTotal = order.OrderTotal,
                                                OrderStatus = order.OrderStatus,
                                                OrderDate = Convert.ToDateTime(order.OrderDate),
                                                OrderDetailID = detail.OrderDetailID,
                                                ProductID = detail.ProductID,
                                                Quantity = detail.Quantity,
                                                TotalPrice = detail.TotalPrice
                                            }).ToList();

                // Return the combined order details list
                return Ok(orderWithDetailsList);
            }
            catch (Exception ex)
            {
                // Log the error or handle exception appropriately
                return InternalServerError(new Exception("Failed to retrieve orders for the given VendorID.", ex));
            }
        }



    }
}
