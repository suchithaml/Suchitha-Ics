using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class VendorNotificationController : Controller
    {
        private readonly NotificationService _notificationService = new NotificationService();

        // GET: Notification
        public async Task<ActionResult> Index()
        {
            try
            {
                var notifications = await _notificationService.GetNotificationsAsync();
                return View(notifications);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading notifications: {ex.Message}";
                return View(new List<NotificationViewModel>());
            }
        }
    }
}
