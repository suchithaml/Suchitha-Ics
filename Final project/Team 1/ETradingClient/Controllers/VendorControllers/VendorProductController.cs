using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Services;
using ETradingClient.Models;


namespace ETradingClient.Controllers
{
    public class VendorProductController : Controller
    {
        private VendorProductService _productService = new VendorProductService();

        // GET: Product
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetProductsAsync(Convert.ToInt32(Session["UserID"]));
            return View(products);
        }

        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            Product prod = new Product();
            prod.listCatgories = await _productService.GetAllCategoriesAsync();
            return View(prod);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddProductAsync(product);
                TempData["Message"] = result;
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound("Product not found.");
            }

            return View(product);
        }

        // POST: Product/update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var resultMessage = await _productService.UpdateProductAsync(id, product);
                TempData["Message"] = resultMessage;
                return RedirectToAction("Index", "VendorProduct");
            }

            return View(product);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return HttpNotFound("Product not found.");
            }

            return View(product);
        }

        // POST: Product/Delete/{id}
        [HttpPost, ActionName("delete")]
        //[HttpDelete ]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            TempData["Message"] = result;
            return RedirectToAction("Index", "VendorProduct");
        }
    }
}