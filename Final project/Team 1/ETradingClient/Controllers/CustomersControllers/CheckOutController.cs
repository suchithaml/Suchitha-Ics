using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETrading.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CheckoutService _checkoutService = new CheckoutService();
        private readonly TopUpService _topUpService = new TopUpService(); // Adding TopUpService for balance check

        private readonly ProductService _productService = new ProductService(); // Adding TopUpService for balance check

        // GET: Checkout
        public ActionResult Index()
        {
            try
            {
                // Ensure the user is logged in by checking session
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Get the cart from session
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    ViewBag.Message = "Your cart is empty.";
                    return RedirectToAction("Index", "Cart");
                }

                return View(cart); // Render checkout view with cart items
            }
            catch (Exception ex)
            {
                // Log exception
                System.Diagnostics.Debug.WriteLine($"Error in Checkout/Index: {ex.Message}");
                TempData["Error"] = "An error occurred while loading the checkout page. Please try again.";
                return RedirectToAction("Index", "Cart");
            }
        }

        // POST: Checkout
        [HttpPost]
        public async Task<ActionResult> Process(int customerId)
        {
            try
            {
                // Check if user is logged in
                if (Session["Username"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Fetch the cart
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    TempData["Message"] = "Your cart is empty.";
                    return RedirectToAction("Index", "Cart");
                }

                // Get user balance using TopUpService
                var userBalance = await _topUpService.GetUserBalanceAsync(customerId);
                decimal orderTotal = cart.Sum(c => c.Price * c.Quantity);

                // Check if the user has sufficient balance
                if (userBalance < orderTotal)
                {
                    TempData["Error"] = "Insufficient balance to complete the purchase.";
                    TempData["Message"] = "Sorry, insufficient balance to complete the purchase.";
                    return RedirectToAction("Failure");
                }

                // Dynamically determine VendorID for each product
                var vendorId = await _productService.GetVendorIdFromProductAsync(cart.Select(c => c.ProductID).FirstOrDefault());
                if (vendorId == null)
                {
                    TempData["Error"] = "Unable to determine vendor for the products in your cart.";
                    return RedirectToAction("Index", "Cart");
                }

                // Prepare checkout model
                var checkout = new CheckOutModel
                {
                    CustomerID = customerId,
                    VendorID = vendorId.Value,
                    OrderTotal = orderTotal,
                    OrderDetails = cart.Select(c => new OrderDetailModel
                    {
                        ProductID = c.ProductID,
                        Quantity = c.Quantity,
                        TotalPrice = c.Price * c.Quantity
                    }).ToList()
                };

                // Call the API service to process the checkout
                var response = await _checkoutService.ProcessCheckoutAsync(checkout);

                // Clear cart on success
                Session["Cart"] = null;
                TempData["Message"] = "Checkout completed successfully!";
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                // Log exception
                System.Diagnostics.Debug.WriteLine($"Error in Checkout/Process: {ex.Message}");
                TempData["Error"] = "An error occurred while processing your checkout. Please try again.";
                return RedirectToAction("Index", "Cart");
            }
        }
        // GET: Checkout/Success
        public ActionResult Success()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = TempData["Message"] ?? "Your order has been placed successfully.";
            return View();
        }

        public ActionResult Failure()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = TempData["Message"] ?? "Sorry, Order not placed successfully. Check Balance.";
            return View();
        }
    }
}