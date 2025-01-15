using System;
using System.Linq;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/notifications")]
    public class VendorNotificationsController : ApiController
    {
        private ETradingDBEntities _context = new ETradingDBEntities();

        [RoutePrefix("api/notification")]
        public class NotificationController : ApiController
        {
            private readonly ETradingDBEntities _context = new ETradingDBEntities();

            // GET: api/notification/show
            [HttpGet]
            [Route("show")]
            public IHttpActionResult GetAllNotifications()
            {
                try
                {
                    var notifications = _context.Notifications.ToList();
                    return Ok(notifications);
                }
                catch (Exception ex)
                {
                    return InternalServerError(new Exception("Error retrieving notifications.", ex));
                }
            }

            // POST: api/notification/add
            //[HttpPost]
            //[Route("add")]
            //public IHttpActionResult AddNotification([FromBody] Notification notification)
            //{
            //    if (notification == null)
            //        return BadRequest("Invalid notification data.");

            //    try
            //    {
            //        _context.Notifications.Add(notification);
            //        _context.SaveChanges();
            //        return Ok(new { Message = "Notification added successfully." });
            //    }
            //    catch (Exception ex)
            //    {
            //        return InternalServerError(new Exception("Error adding notification.", ex));
            //    }
            //}

            // Trigger this when a product's price changes

            //public void CreatePriceAlert(int productId, decimal oldPrice, decimal newPrice)
            //{
            //    // Only create notification if the price has changed
            //    if (oldPrice != newPrice)
            //    {
            //        var notification = new Notification
            //        {
            //            ProductID = productId,
            //            OldPrice = oldPrice,
            //            NewPrice = newPrice,
            //          //  Content = $"Price for product {productId} changed from {oldPrice} to {newPrice}",
            //            UpdatedAt = DateTime.Now
            //        };

            //        _context.Notifications.Add(notification);
            //        _context.SaveChanges();
            //    }
            //}
        }
    }
}