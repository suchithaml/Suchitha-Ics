using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class TopUpController : Controller
    {
        private readonly TopUpService _topUpService = new TopUpService();

        // Render the Top-Up page
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            ViewBag.Username = Session["Username"].ToString();
            ViewBag.UserID = Session["UserID"];
            return View();
        }

        // Get user balance (AJAX request)
        public async Task<JsonResult> GetBalance(int userId)
        {
            if (Session["Username"] == null)
            {
                return Json(new { error = "User not logged in." }, JsonRequestBehavior.AllowGet);
            }

            var balance = await _topUpService.GetUserBalanceAsync(userId);
            return Json(new { balance }, JsonRequestBehavior.AllowGet);
        }

        // Process the top-up (add balance)
        [HttpPost]
        public async Task<JsonResult> AddBalance(TopUpRequest request)
        {
            if (Session["Username"] == null)
            {
                return Json(new { error = "User not logged in." }, JsonRequestBehavior.AllowGet);
            }

            if (request.UserID <= 0 || request.Amount <= 0)
            {
                return Json(new { error = "Invalid input." }, JsonRequestBehavior.AllowGet);
            }

            var result = await _topUpService.AddBalanceAsync(request);

            return Json(new { success = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
        }

      
    }
}