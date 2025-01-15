using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETradingClient.Models;

public class WishlistController : Controller
{
    private List<WishListItem> GetWishlist()
    {
        var wishlist = Session["WishList"] as List<WishListItem>;
        if (wishlist == null)
        {
            wishlist = new List<WishListItem>();
            Session["WishList"] = wishlist;
        }
        return wishlist;
    }

    [HttpPost]
    public ActionResult AddToWishlist(int productId, string productName, decimal price, string imageUrl)
    {
        var wishlist = GetWishlist();
        var existingItem = wishlist.FirstOrDefault(w => w.ProductID == productId);

        if (existingItem == null)
        {
            var newItem = new WishListItem
            {
                ProductID = productId,
                ProductName = productName,
                Price = price,
                ImageUrl = imageUrl
            };
            wishlist.Add(newItem);
        }

        return RedirectToAction("Index", "WishList");
    }

    public ActionResult Index()
    {
        var wishlist = GetWishlist();
        return View(wishlist);
    }

    [HttpPost]
    public ActionResult RemoveFromWishlist(int productId)
    {
        var wishlist = GetWishlist();
        var itemToRemove = wishlist.FirstOrDefault(w => w.ProductID == productId);
        if (itemToRemove != null)
        {
            wishlist.Remove(itemToRemove);
        }
        return RedirectToAction("Index", "WishList");
    }
}

