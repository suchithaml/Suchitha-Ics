using System;
using System.Linq;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers.Customer
{
    [RoutePrefix("api/custnotifications")]
    public class CustomerNotificationsController : ApiController
    {
        private readonly ETradingDBEntities _context = new ETradingDBEntities(); // Your database context

        // GET: api/notifications
        [HttpGet]
        [Route("notifications")]
        public IHttpActionResult GetNotificationsToday()
        {
            var notifications = _context.Database
                .SqlQuery<NotificationViewModel>("GetNotificationsToday")
                .ToList();

            if (notifications == null || !notifications.Any())
            {
                return NotFound();
            }

            return Ok(notifications);
        }
    }
}