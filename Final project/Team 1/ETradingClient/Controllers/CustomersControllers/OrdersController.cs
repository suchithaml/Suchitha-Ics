using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public OrdersController()
        {
            _orderService = new OrderService();
            _userService = new UserService();
        }

        // Checkout Action
        [HttpPost]
        public async Task<ActionResult> Checkout()
        {
            // Retrieve UserID from session
            if (Session["UserID"] == null)
            {
                TempData["Error"] = "You must be logged in to proceed.";
                return RedirectToAction("Login", "Account");
            }

            int customerId = (int)Session["UserID"]; // Fetch UserID directly

            // Check if the user has sufficient balance
            var balance = await _orderService.GetUserBalanceAsync(customerId);
            var cart = await _orderService.GetCartItemsAsync(customerId);
            decimal orderTotal = cart.Sum(c => c.Quantity * c.Price);

            if (balance < orderTotal)
            {
                TempData["Error"] = "Insufficient balance. Please top up.";
                return RedirectToAction("Search", "Products");
            }

            // Place the order
            var orderId = await _orderService.PlaceOrderAsync(customerId, cart);

            if (orderId > 0)
            {
                TempData["Success"] = "Order placed successfully!";
                return RedirectToAction("OrderDetails", new { id = orderId });
            }

            TempData["Error"] = "Failed to place order. Please try again.";
            return RedirectToAction("Index", "Cart");
        }
        // Order Details Action
        [HttpGet]
        public async Task<ActionResult> OrderDetails(int id)
        {
            try
            {
                var orderDetails = await _orderService.GetOrderDetailsAsync(id);
                if (orderDetails == null)
                {
                    TempData["Error"] = "Order details not found.";
                    return RedirectToAction("Cart", "Products");
                }

                return View(orderDetails);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Cart", "Products");
            }
        }

        [HttpGet]
        public async Task<ActionResult> OrderHistory()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not logged in
            }
            int customerId = (int)Session["UserID"]; // Get the logged-in user's ID from the session
            var orders = await _orderService.GetOrderHistoryAsync(customerId);

            if (orders == null || orders.Count == 0)
            {
                ViewBag.Message = "No order history found.";
                return View();
            }

            return View(orders); // Pass the order history to the view
        }
    }
}