using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETradingClient.Models;

public class CartController : Controller
{
    // Fetch the cart for the current user from the session
    private List<CartItem> GetCart()
    {
        if (Session["Username"] == null)
        {
            RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
        }
        ViewBag.Username = Session["Username"].ToString(); // Pass username to the view

        var cart = Session["Cart"] as List<CartItem>;
        if (cart == null)
        {
            cart = new List<CartItem>();
            Session["Cart"] = cart;
        }
        return cart;
    }

    // Add a product to the cart
    [HttpPost]
    public ActionResult AddToCart(int productId, string productName, decimal price, string imageUrl, int quantity)
    {
        if (Session["Username"] == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
        }
        ViewBag.Username = Session["Username"].ToString(); // Pass username to the view

        var cart = GetCart();
        var existingItem = cart.FirstOrDefault(c => c.ProductID == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity; // Increase quantity if product already in the cart
        }
        else
        {
            var newItem = new CartItem
            {
                ProductID = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                ImageUrl = imageUrl
            };
            cart.Add(newItem); // Add new item to cart
        }

        TempData["Message"] = "Item added to cart successfully!";
        return RedirectToAction("Index", "Cart");
    }

    // Display the cart items
    public ActionResult Index()
    {
        if (Session["Username"] == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
        }

        var cart = GetCart();
        ViewBag.Username = Session["Username"].ToString(); // Pass username to the view
        return View(cart);
    }

    // Remove an item from the cart
    [HttpPost]
    public ActionResult RemoveFromCart(int productId)
    {
        if (Session["Username"] == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
        }

        var cart = GetCart();
        var itemToRemove = cart.FirstOrDefault(c => c.ProductID == productId);
        System.Diagnostics.Debug.WriteLine($"{itemToRemove}");

        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
        }

        TempData["Message"] = "Item removed from cart successfully!";
        return RedirectToAction("Index", "Cart");
    }
}
