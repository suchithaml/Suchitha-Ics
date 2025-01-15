using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Linq;
using System;
using Newtonsoft.Json;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/vendorProfile")]
    public class VendorProfileController : ApiController
    {
        private readonly ETradingDBEntities _context = new ETradingDBEntities();

        // POST: api/vendorProfile/add
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddOrUpdateProfile([FromBody] Vendor vendor)
        {
            if (vendor == null || vendor.VendorID <= 0)
                return BadRequest("Invalid vendor data. VendorID must be provided.");

            try
            {
                // Check if the vendor already exists in the database
                var existingVendor = _context.Vendors.FirstOrDefault(v => v.VendorID == vendor.VendorID);

                if (existingVendor == null)
                {
                    // Add a new vendor profile
                    _context.Vendors.Add(new Vendor
                    {
                        VendorID = vendor.VendorID, // Set VendorID explicitly
                        VendorName = vendor.VendorName,
                        VendorCompanyPhoneNo = vendor.VendorCompanyPhoneNo,
                        VendorCompanyAddress = vendor.VendorCompanyAddress,
                        VendorCompanyEmailID = vendor.VendorCompanyEmailID,
                        VendorCompanyName = vendor.VendorCompanyName,
                        VendorIsActive = vendor.VendorIsActive, // Optional, defaults to 1
                        CustomerID = vendor.CustomerID // Optional, defaults to -1
                    });
                }
                else
                {
                    // Update the existing vendor profile
                    existingVendor.VendorName = vendor.VendorName;
                    existingVendor.VendorCompanyPhoneNo = vendor.VendorCompanyPhoneNo;
                    existingVendor.VendorCompanyAddress = vendor.VendorCompanyAddress;
                    existingVendor.VendorCompanyEmailID = vendor.VendorCompanyEmailID;
                    existingVendor.VendorCompanyName = vendor.VendorCompanyName;
                    existingVendor.VendorIsActive = vendor.VendorIsActive;
                    existingVendor.CustomerID = vendor.CustomerID;
                }


                // Save changes to the database
                _context.SaveChanges();

                return Ok(new { Message = "Profile saved successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (use your logging framework or strategy here)
                return InternalServerError(new Exception("Error saving profile. Please try again later.", ex));
            }
        }

        [HttpGet]
        [Route("{vendorId}")]
        public IHttpActionResult GetVendorProfile(int vendorId)
        {
            // Ensure the vendorId exists in the Users table
            var userExists = _context.Users.Any(u => u.UserID == vendorId);
            if (!userExists)
            {
                return NotFound();  // Return 404 if UserID doesn't exist in Users table
            }

            // Fetch the vendor details from the vendor table
            var vendor = _context.Vendors
     .Where(v => v.VendorID == vendorId)
     .Select(v => new
     {
         v.VendorID,
         v.VendorName,
         v.VendorCompanyPhoneNo,
         v.VendorCompanyAddress,
         v.VendorCompanyEmailID,
         v.VendorCompanyName,
         v.VendorIsActive,
         v.CustomerID
     })
     .FirstOrDefault();

            if (vendor == null)
            {
                return NotFound(); // Return 404 if vendor is not found
            }

            return Ok(vendor);
            // Return the vendor profile
        }
    }
}