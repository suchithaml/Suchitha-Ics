using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class VendorOrdersController : Controller
    {
        private readonly VendorOrderService _orderService = new VendorOrderService();



        // Action to display orders with details
        //public async Task<ActionResult> OrdersWithDetails()
        //{
        //    try
        //    {

        //        //var orders = await _orderService.GetAllOrdersWithDetailsAsync();
        //        return View(new OrderDetailsViewModel());
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = ex.Message;
        //        return View("Error");
        //    }
        //}

        public async Task<ActionResult> OrdersWithDetails()
        {
            // Check if the user is logged in
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // Retrieve UserID from session
                int vendorId = Convert.ToInt32(Session["UserID"]);

                // Fetch orders for the logged-in user
                var orders = await _orderService.GetOrdersByUserIdAsync(vendorId);

                // If no orders are found, return an empty view with a message
                if (orders == null || orders.Count == 0)
                {
                    ViewData["NoOrdersMessage"] = "You have no orders yet.";  // Set a message indicating no orders
                    return View(new List<OrderwithDetails>());  // Return an empty list to the view
                }

                // Return the view with the list of orders
                return View(orders);
            }
            catch (Exception ex)
            {
                // Handle exception and log it if necessary
                ViewBag.ErrorMessage = "An error occurred while fetching your orders. Please try again later.";
                return View("Error");
            }
        }

    }
}
