using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/ActivationDeactivationVendor")]
    public class ActivationDeactivationVendorController : ApiController
    {
        private ETradingDBEntities db = new ETradingDBEntities();

        // GET: api/ActivationDeactivationVendor
        [HttpGet]
        public IHttpActionResult GetVendors()
        {
            var vendors = db.Vendors.ToList();
            return Ok(vendors);
        }

        // GET: api/ActivationDeactivationVendor/GetVendorDetails
        [Route("GetVendorDetails/{id}")]
        [HttpGet]
        public IHttpActionResult GetVendorDetails(int id)
        {
            var vendor = db.Vendors.FirstOrDefault(v => v.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            //vendor.VendorIsActive = false;
            //db.SaveChanges();
            return Ok(vendor);
        }
    

    // PUT: api/ActivationDeactivationVendor/Activate/id
    [HttpPut]
        [Route("Activate/{id}")]
        public IHttpActionResult ActivateVendor(int id)
        {
            var vendor = db.Vendors.FirstOrDefault(v => v.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            vendor.VendorIsActive = true;
            db.SaveChanges();
            return Ok(vendor);
        }

        // PUT: api/ActivationDeactivationVendor/Deactivate/5
        [HttpPut]
        [Route("Deactivate/{id}")]
        public IHttpActionResult DeactivateVendor(int id)
        {
            var vendor = db.Vendors.FirstOrDefault(v => v.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            vendor.VendorIsActive = false;
            db.SaveChanges();
            return Ok(vendor);
        }

        // PUT: api/ActivationDeactivationVendor/Update/5
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult UpdateVendor(Vendor vendor)
        {
            //if (id != vendor.VendorID)
            //{
            //    return BadRequest();
            //}

            db.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(vendor);
        }

        // DELETE: api/ActivationDeactivationVendor/5
        [HttpDelete]
        public IHttpActionResult DeleteVendor(int id)
        {
            var vendor = db.Vendors.FirstOrDefault(v => v.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            vendor.VendorIsActive = false; 
            db.SaveChanges();
            return Ok(vendor);
        }
    }
}
