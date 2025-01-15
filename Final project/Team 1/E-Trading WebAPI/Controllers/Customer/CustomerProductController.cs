using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/custproducts")]
    public class CustomerProductsController : ApiController
    {
        private ETradingDBEntities context = new ETradingDBEntities();

        // Endpoint for search suggestions
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchProducts(string query)
        {
            var products = (from product in context.Products
                            join category in context.Categories
                            on product.CategoryID equals category.CategoryID
                            where product.ProductName.Contains(query) || category.CategoryName.Contains(query)
                            select new
                            {
                                product.ProductName
                            })
                            .Distinct()
                            .ToList();

            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        // Endpoint for detailed search
        [HttpGet]
        [Route("search/details")]
        public IHttpActionResult SearchProductsDetails(string query)
        {
            //var products = (from product in context.Products
            //                join category in context.Categories
            //                on product.CategoryID equals category.CategoryID
            //                where product.ProductName.Contains(query) || category.CategoryName.Contains(query) 
            //                select new
            //                {
            //                    product.ProductID,
            //                    product.ProductName,
            //                    product.Description,
            //                    product.Price,
            //                    product.Stock,
            //                    product.ImagePath,
            //                    CategoryName = category.CategoryName
            //                })
            //                .ToList();

            var products = (from product in context.Products
                            join category in context.Categories
                                on product.CategoryID equals category.CategoryID
                            join vendor in context.Vendors
                                on product.VendorID equals vendor.VendorID
                            where (product.ProductName.Contains(query) || category.CategoryName.Contains(query))
                                  && vendor.VendorIsActive == true
                            select new
                            {
                                product.ProductID,
                                product.ProductName,
                                product.Description,
                                product.Price,
                                product.Stock,
                                product.ImagePath,
                                CategoryName = category.CategoryName
                            })
                .ToList();


            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        // Get details of a specific product
        [HttpGet]
        [Route("details/{id}")]
        public IHttpActionResult GetProductDetails(int id)
        {
            var product = context.Database
                .SqlQuery<ProductViewModel>("EXEC GetProductDetails @ProductID", new SqlParameter("@ProductID", id))
                .FirstOrDefault();

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // Get products by category
        [HttpGet]
        [Route("byCategory")]
        public IHttpActionResult GetProductsByCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Category name cannot be null or empty.");
            }

            var products = (from product in context.Products
                            join category in context.Categories
                            on product.CategoryID equals category.CategoryID
                            where category.CategoryName == categoryName
                            select new
                            {
                                product.ProductID,
                                product.ProductName,
                                product.Description,
                                product.Price,
                                product.Stock,
                                product.ImagePath,
                                CategoryName = category.CategoryName
                            })
                            .ToList();

            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        [Route("vendor/{productId}")]
        public IHttpActionResult GetVendorByProduct(int productId)
        {
            try
            {
                // Fetch product from the database
                var product = context.Products.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    return NotFound(); // Return 404 if product is not found
                }

                return Ok(new { VendorID = product.VendorID });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use your logging mechanism here)
                return InternalServerError(new Exception("An error occurred while fetching the vendor information.", ex));
            }
        }
    }
}