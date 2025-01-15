using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/admincategories")]
    public class AdminCategoriesController : ApiController
    {
        private ETradingDBEntities context = new ETradingDBEntities();

        // GET: api/admincategories/ShowCategories
        [HttpGet]
        [Route("ShowCategories")]
        public async Task<IHttpActionResult> GetAllCategories()
        {
            var categories = await context.Categories.ToListAsync();

            if (categories.Count == 0)
                return Content(HttpStatusCode.NotFound, "No categories available");

            return Ok(categories);
        }

        // GET: api/admincategories/{CategoryID}
        [HttpGet]
        [Route("{CategoryID}", Name = "GetCategoryDetails")]
        public async Task<IHttpActionResult> GetCategoryDetails(int CategoryID)
        {
            var category = await context.Categories.FindAsync(CategoryID);

            if (category == null)
                return Content(HttpStatusCode.NotFound, "Category not found with the given ID.");

            return Ok(category);
        }

        // POST: api/admincategories/CreateCategory
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> CreateCategory([FromBody] Category cat)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input. Please check your data.");

            var category = await context.Categories.Where(c => c.CategoryName.Contains(cat.CategoryName)).ToListAsync();
            if (category.Count == 0)
            {
                context.Categories.Add(cat);
                await context.SaveChangesAsync();
                return CreatedAtRoute("GetCategoryDetails", new { CategoryID = cat.CategoryID }, cat);
            }
            else
            {
                return BadRequest("Already Category is exists");
            }


            
        }

        // PUT: api/admincategories/UpdateCategory
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IHttpActionResult> UpdateCategory([FromBody] Category cat)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input. Please check your data.");

            context.Entry(cat).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok("Update Successful");
        }

        // DELETE: api/admincategories/DeleteCategory/{CategoryID}
        [HttpDelete]
        [Route("DeleteCategory/{CategoryID}")]
        public async Task<IHttpActionResult> DeleteCategory(int CategoryID)
        {
            var category = await context.Categories.FindAsync(CategoryID);

            if (category == null)
                return Content(HttpStatusCode.NotFound, "Category not found with the given ID.");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok("Category deleted successfully");
        }
    }
}
