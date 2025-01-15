using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService = new ProductService();

        // Render Search Page
        public ActionResult Search()
        {
            // Ensure the user is logged in by checking session
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if session is null
            }

            ViewBag.Username = Session["Username"].ToString(); // Pass username to the view
            ViewBag.UserID = Session["UserID"]; // Pass username to the view

            return View();
        }

        // Fetch suggestions dynamically
        public async Task<JsonResult> GetSuggestions(string query)
        {
            // Ensure the user is logged in
            if (Session["Username"] == null)
            {
                return Json(new { error = "User not logged in." }, JsonRequestBehavior.AllowGet);
            }

            var suggestions = await _productService.GetSuggestionsAsync(query);
            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        // Fetch and render product search results
        public async Task<ActionResult> SearchResults(string query)
        {
            // Ensure the user is logged in
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Username = Session["Username"].ToString(); // Pass username to the view
            var products = await _productService.SearchProductsAsync(query);
            ViewBag.Query = query;
            return View(products);
        }

        // Render product details
        public async Task<ActionResult> Details(int id)
        {
            // Ensure the user is logged in
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = await _productService.GetProductDetailsAsync(id);

            if (product == null)
            {
                ViewBag.Error = "Product not found.";
                return View();
            }

            ViewBag.Username = Session["Username"].ToString(); // Pass username to the view
            return View(product);
        }
        public async Task<ActionResult> ProductsByStaticCategory(string categoryName)
        {
            // Ensure the user is logged in
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Username = Session["Username"].ToString(); // Pass username to the view
            var products = await _productService.GetProductsByCategoryAsync(categoryName);
            ViewBag.CategoryName = categoryName; // Pass category name to the view
            return View("ProductsByCategory", products); // Reuse the existing view for products
        }

    }
}
