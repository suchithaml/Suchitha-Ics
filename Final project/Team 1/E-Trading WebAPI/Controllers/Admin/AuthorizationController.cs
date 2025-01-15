using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/Authorization")]
    public class AuthorizationController : ApiController
    {
        private readonly ETradingDBEntities _context = new ETradingDBEntities();

        // GET: api/Authorization/GetPendingVendors
        [HttpGet]
        [Route("GetPendingVendors")]
        public IHttpActionResult GetPendingVendors()
        {
            var pendingVendors = _context.Users
                .Join(_context.Vendors,
                      user => user.UserID,
                      vendor => vendor.VendorID,
                      (user, vendor) => new
                      {
                          user.UserID,
                          user.Username,
                          vendor.VendorID,
                          vendor.VendorName,
                          vendor.VendorCompanyName,
                          vendor.VendorCompanyAddress,
                          vendor.VendorCompanyEmailID,
                          vendor.VendorCompanyPhoneNo,
                          user.Status,
                          user.IsVendor
                      })
                .Where(x => x.IsVendor == true &&
                            (x.Status == "Pending" || x.Status == null))
                .ToList();

            if (!pendingVendors.Any())
                return NotFound();

            return Ok(pendingVendors);
        }

        [HttpPut]
        [Route("UpdateAuthorizationStatus")]
        public IHttpActionResult UpdateAuthorizationStatus(int UserID, string status)
        {
            var vendor = _context.Users.FirstOrDefault(v => v.UserID == UserID);
            if (vendor == null)
            {
                return NotFound();
            }
            vendor.Status = status;
            _context.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return Ok("Succesfully authorized the user.");
        }
    }
}
