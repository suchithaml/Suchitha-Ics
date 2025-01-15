using ETradingClient.Models;
using ETradingClient.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ETradingClient.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly NotificationService _service = new NotificationService();

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            var notifications = await _service.GetNotificationsAsync();

            if (notifications == null || notifications.Count == 0)
            {
                ViewBag.Message = "No notifications available for today.";
                return View();
            }

            return View(notifications);
        }
    }
}