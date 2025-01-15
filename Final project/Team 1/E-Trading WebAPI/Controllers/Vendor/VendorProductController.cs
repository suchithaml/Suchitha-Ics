using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
//using static vendor_server.Controllers.NotificationsController;
using System.Data.Entity.Validation;
using E_Trading_WebAPI.Models;
using System.Threading.Tasks;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class VendorProductController : ApiController
    {
        private readonly ETradingDBEntities _context = new ETradingDBEntities();

        // POST: api/product/add
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.ImagePath))
                return BadRequest("Invalid product data. ImagePath is required.");

            try
            {
                _context.Products.Add(new Product
                {
                    VendorID = product.VendorID,
                    CategoryID = product.CategoryID,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description=product.Description,
                    ImagePath = product.ImagePath, // Assigning the ImagePath field
                    CreatedAt = DateTime.Now
                });

                _context.SaveChanges();
                return Ok(new { Message = "Product added successfully." });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error adding product. Please try again later.", ex));
            }
        }

        [HttpGet]
        [Route("show/{vendorId}")]
        public IHttpActionResult GetAllProducts(int vendorId)
        {
            try
            {
                var products = _context.Products.Where(p => p.VendorID == vendorId).ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Failed to retrieve products.", ex));
            }
        }

        [HttpGet]
        [Route("vendor/{vendorId}")]
        public IHttpActionResult GetProductsByVendorId(int vendorId)
        {
            try
            {
                var products = _context.Products.Where(p => p.VendorID == vendorId).ToList();

                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Failed to retrieve products for the given VendorID.", ex));
            }
        }


        // PUT: api/product/update/{productId}
        //[HttpPut]
        //[Route("update/{productId}")]
        //public IHttpActionResult UpdateProduct(int productId, [FromBody] Product product)
        //{
        //    if (product == null)
        //        return BadRequest("Invalid product data.");
        //    try
        //    {
        //        // Fetch the existing product
        //        var existingProduct = _context.Products.FirstOrDefault(p => p.ProductID == productId);
        //        if (existingProduct == null)
        //            return NotFound();
        //        // Store the old price for comparison
        //        decimal oldPrice = existingProduct.Price;
        //        // Check if the price has changed
        //        bool isPriceChanged = oldPrice != product.Price;
        //        // Update product details
        //        existingProduct.VendorID = product.VendorID;
        //        existingProduct.CategoryID = product.CategoryID;
        //        existingProduct.ProductName = product.ProductName;
        //        existingProduct.Price = product.Price;
        //        existingProduct.Stock = product.Stock;
        //        existingProduct.ImagePath = product.ImagePath;
        //        // Save the changes
        //        _context.Entry(existingProduct).State = EntityState.Modified;
        //        _context.SaveChanges();

        //        // If the price has changed, add an entry to the Notifications table

        //        if (isPriceChanged)
        //        {
        //            var notification = new Notification
        //            {
        //                ProductID = productId,
        //                OldPrice = oldPrice,
        //                NewPrice = product.Price,
        //                Content = $"Price for product '{existingProduct.ProductName}' changed from {oldPrice:C} to {product.Price:C}.",
        //                UpdatedAt = DateTime.Now
        //            };
        //            _context.Notifications.Add(notification);
        //            _context.SaveChanges();
        //        }
        //        return Ok(new { Message = "Product updated successfully." });
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        // Capture all validation errors
        //        var errorMessages = ex.EntityValidationErrors
        //            .SelectMany(x => x.ValidationErrors)
        //            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

        //        var fullErrorMessage = string.Join("; ", errorMessages);
        //        var exceptionMessage = $"Validation errors: {fullErrorMessage}";

        //        // Log or rethrow a more informative exception
        //        throw new Exception(exceptionMessage, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(new Exception("Error updating product. Please try again later.", ex));
        //    }
        //}

        [HttpPut]
        [Route("update/{productId}")]
        public IHttpActionResult UpdateProduct(int productId, [FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Invalid product data.");

            try
            {
                // Fetch the existing product
                var existingProduct = _context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (existingProduct == null)
                    return NotFound();

                // Store the old price for comparison
                decimal oldPrice = existingProduct.Price;

                // Check if the price has changed
                bool isPriceChanged = oldPrice != product.Price;

                // Update product details
                existingProduct.VendorID = product.VendorID;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.ImagePath = product.ImagePath;

                // Save the changes to the product
                _context.Entry(existingProduct).State = EntityState.Modified;
                _context.SaveChanges();

                // If the price has changed, add an entry to the Notifications table
                if (isPriceChanged)
                {
                    var notification = new Notification
                    {
                        ProductID = productId,
                        OldPrice = oldPrice,
                        NewPrice = product.Price,
                        UpdatedAt = DateTime.Now
                    };

                    _context.Notifications.Add(notification);
                    _context.SaveChanges();
                }

                return Ok(new { Message = "Product updated successfully." });
            }
            catch (DbEntityValidationException ex)
            {
                // Capture all validation errors
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = $"Validation errors: {fullErrorMessage}";

                // Log or rethrow a more informative exception
                throw new Exception(exceptionMessage, ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error updating product. Please try again later.", ex));
            }
        }





        // DELETE: api/product/delete/{productId}
        [HttpDelete]
        [Route("delete/{productId}")]
        public IHttpActionResult DeleteProduct(int productId)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    return NotFound();
                }
                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok(new { Message = "Product deleted successfully." });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error deleting product. Please try again later.", ex));
            }
        }


        [HttpGet]
        [Route("{productId}")]
        public IHttpActionResult GetProductById(int productId)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Failed to retrieve the product with the given ProductID.", ex));
            }
        }


        [HttpGet]
        [Route("VendorProductAddShowCategories")]
        public async Task<IHttpActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories.Count == 0)
                return Content(System.Net.HttpStatusCode.NotFound, "No categories available");

            return Ok(categories);
        }
    }
}