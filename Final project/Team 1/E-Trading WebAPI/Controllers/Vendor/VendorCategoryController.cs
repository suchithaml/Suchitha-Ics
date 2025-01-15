using System;
using System.Linq;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/categories")]
    public class VendorCategoryController : ApiController
    {
        private readonly ETradingDBEntities _context = new ETradingDBEntities();

        [HttpGet]
        [Route("show")]
        public IHttpActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
                return NotFound();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddCategory([FromBody] Category category)
        {
            if (category == null || string.IsNullOrWhiteSpace(category.CategoryName))
                return BadRequest("Invalid category data.");
            if (_context.Categories.Any(c => c.CategoryName == category.CategoryName))
                return BadRequest("Category name already exists.");
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Category added successfully.");
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public IHttpActionResult UpdateCategory(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null || string.IsNullOrWhiteSpace(updatedCategory.CategoryName))
                return BadRequest("Invalid category data.");
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
                return NotFound();
            if (_context.Categories.Any(c => c.CategoryName == updatedCategory.CategoryName && c.CategoryID != id))
                return BadRequest("Category name already exists.");
            category.CategoryName = updatedCategory.CategoryName;
            _context.SaveChanges();
            return Ok("Category updated successfully.");
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
                return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Category deleted successfully.");
        }
    }
}
