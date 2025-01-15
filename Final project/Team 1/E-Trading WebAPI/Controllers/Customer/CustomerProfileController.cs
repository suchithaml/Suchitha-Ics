using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Linq;
using System;
using Newtonsoft.Json;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/custprofile")]
    public class CustomerProfileController : ApiController
    {
        private readonly ETradingDBEntities _context1;

        public CustomerProfileController()
        {
            _context1 = new ETradingDBEntities();
        }

        // POST: api/profile/add
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddOrUpdateProfile([FromBody] E_Trading_WebAPI.Models.Customer customer)
        {
            if (customer == null || customer.CustomerID <= 0)
                return BadRequest("Invalid customer data. CustomerID must be provided.");

            try
            {
                // Check if the customer already exists in the database
                var existingCustomer = _context1.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);

                if (existingCustomer == null)
                {
                    // Add a new customer profile
                    _context1.Customers.Add(new E_Trading_WebAPI.Models.Customer
                    {
                        CustomerID = customer.CustomerID, // Set CustomerID explicitly
                        CustomerName = customer.CustomerName,
                        CustomerEmailID = customer.CustomerEmailID,
                        CustomerPhoneNo = customer.CustomerPhoneNo,
                        CustomerAddress = customer.CustomerAddress
                    });
                }
                else
                {
                    // Update the existing customer profile
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.CustomerEmailID = customer.CustomerEmailID;
                    existingCustomer.CustomerPhoneNo = customer.CustomerPhoneNo;
                    existingCustomer.CustomerAddress = customer.CustomerAddress;
                }

                // Save changes to the database
                _context1.SaveChanges();

                return Ok(new { Message = "Profile saved successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (use your logging framework or strategy here)
                return InternalServerError(new Exception("Error saving profile. Please try again later.", ex));
            }
        }

        [HttpGet]
        [Route("profile/{customerId}")]
        public IHttpActionResult GetCustomerProfile(int customerId)
        {
            // Ensure the customerId exists in the Users table
            var userExists = _context1.Users.Any(u => u.UserID == customerId);
            if (!userExists)
            {
                return NotFound();  // Return 404 if UserID doesn't exist in Users table
            }

            // Fetch the customer details from the Customer table
            var customer = _context1.Customers
                .Where(c => c.CustomerID == customerId)
                .Select(c => new
                {
                    c.CustomerID,
                    c.CustomerName,
                    c.CustomerPhoneNo,
                    c.CustomerAddress,
                    c.CustomerEmailID
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound();  // Return 404 if customer is not found
            }

            return Ok(customer);  // Return the customer profile
        }
    }
}