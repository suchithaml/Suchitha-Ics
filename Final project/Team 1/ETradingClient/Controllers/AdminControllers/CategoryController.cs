using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ETradingClient.Services;
using ETradingClient.Models;

namespace ETradingClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService = new CategoryService();
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories.Count == 0)
            {
                ViewBag.ErrorMessage = "No categories available.";
            }

            return View(categories);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryDetailsAsync(id);

            if (category.CategoryID == 0 || category == null)
            {
                ViewBag.ErrorMessage = "Category not found.";
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(category);

                if (createdCategory != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Category creation failed.";
                }
            }
            else
            {

                var errorMessages = ModelState.Values
                                              .SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();


                ViewBag.ErrorMessage = string.Join("<br />", errorMessages);
            }

            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryDetailsAsync(id);

            if (category.CategoryID == 0 || category == null)
            {
                ViewBag.ErrorMessage = "Category not found.";
                return RedirectToAction("Index");
            }

            return View(category);
        }



        [HttpPost]
        public async Task<ActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = await _categoryService.UpdateCategoryAsync(category);

                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Category update failed.";
                }
            }
            else
            {
               
                var errorMessages = ModelState.Values
                                              .SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

              
                ViewBag.ErrorMessage = string.Join("<br />", errorMessages);
            }

            return View(category);
        }



        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Category deletion failed.";
                return RedirectToAction("Index");
            }
        }



    }
}