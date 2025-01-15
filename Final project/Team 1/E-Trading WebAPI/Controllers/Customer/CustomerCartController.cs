using System;
using System.Linq;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    public class CustomerCheckoutController : ApiController
    {
        // POST: api/checkout
        [HttpPost]
        [Route("api/custcheckout")]
        public IHttpActionResult Checkout([FromBody] CheckOutRequest request)
        {
            // Validate the request
            if (request == null || request.CustomerID <= 0 || request.VendorID <= 0 ||
                request.OrderTotal <= 0 || request.OrderDetails == null || !request.OrderDetails.Any())
            {
                return BadRequest("Invalid input data.");
            }

            using (var dbContext = new ETradingDBEntities()) // Replace with your actual DbContext name
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Fetch customer's current balance
                        var topUp = dbContext.TopUps.FirstOrDefault(t => t.UserID == request.CustomerID);
                        if (topUp == null)
                        {
                            return BadRequest("Customer not found in TopUp table.");
                        }
                        if (topUp.Balance < request.OrderTotal)
                        {
                            return BadRequest("Insufficient balance.");
                        }

                        // Deduct balance
                        topUp.Balance -= request.OrderTotal;
                        dbContext.SaveChanges();

                        // Insert into Orders table
                        var newOrder = new Order
                        {
                            CustomerID = request.CustomerID,
                            VendorID = request.VendorID,
                            OrderTotal = request.OrderTotal,
                            OrderStatus = "Completed",
                            OrderDate = DateTime.Now
                        };
                        dbContext.Orders.Add(newOrder);
                        dbContext.SaveChanges();

                        // Insert into OrderDetails table and update stock in Products table
                        foreach (var detail in request.OrderDetails)
                        {
                            // Check if product exists
                            var product = dbContext.Products.FirstOrDefault(p => p.ProductID == detail.ProductID);
                            if (product == null)
                            {
                                throw new Exception($"Product with ID {detail.ProductID} not found.");
                            }

                            // Check if sufficient stock is available
                            if (product.Stock < detail.Quantity)
                            {
                                throw new Exception($"Insufficient stock for product ID {detail.ProductID}. Available: {product.Stock}, Requested: {detail.Quantity}");
                            }

                            // Deduct stock
                            product.Stock -= detail.Quantity;

                            // Add order details
                            var orderDetail = new OrderDetail
                            {
                                OrderID = newOrder.OrderID,
                                ProductID = detail.ProductID,
                                Quantity = detail.Quantity,
                                TotalPrice = detail.TotalPrice
                            };
                            dbContext.OrderDetails.Add(orderDetail);
                        }

                        dbContext.SaveChanges();

                        // Commit transaction
                        transaction.Commit();

                        return Ok(new { Message = "Order processed successfully!", OrderID = newOrder.OrderID });
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of error
                        transaction.Rollback();
                        return InternalServerError(new Exception("An error occurred while processing the order: " + ex.Message));
                    }
                }
            }
        }
    }
}