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
    public class ActivationDeactivationVendorController : Controller
    {
            private readonly ActivationDeactivationVendorService _vendorService = new ActivationDeactivationVendorService();

            // GET: Vendor
            public async Task<ActionResult> GetAllActivationDeactivationVendor()
            {
                var vendors = await _vendorService.GetAllVendorsAsync();

                if (vendors.Count == 0)
                {
                    ViewBag.ErrorMessage = "No vendors available.";
                }

                return View(vendors);
            }

            // GET: Vendor/Details/5
            [HttpGet]
            [Route("GetActivationDeactivationVendorDetails")]
            public async Task<ActionResult> ActivationDeactivationVendorDetails(int id)
            {
                var vendor = await _vendorService.GetVendorDetailsAsync(id);

                if (vendor == null)
                {
                    ViewBag.ErrorMessage = "Vendor not found.";
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }

                return View(vendor);
            }

        [HttpGet]
        [Route("ActivationDeactivationVendorEdit")]
        public async Task<ActionResult> ActivationDeactivationVendorEdit(int id)
        {
            var vendor = await _vendorService.GetVendorDetailsAsync(id);

            if (vendor == null)
            {
                ViewBag.ErrorMessage = "Vendor not found.";
                return RedirectToAction("GetAllActivationDeactivationVendor");
            }

            return View(vendor);
        }

        // POST: Vendor/Edit/5
        [HttpPost]
            public async Task<ActionResult> ActivationDeactivationVendorEdit(mdlActivationDeactivationVendor vendor)
            {
                if (ModelState.IsValid)
                {
                    var isUpdated = await _vendorService.UpdateVendorAsync(vendor);

                    if (isUpdated)
                    {
                        return RedirectToAction("GetAllActivationDeactivationVendor");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Vendor update failed.";
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

                return View(vendor);
            }

            // GET: Vendor/Activate/5
            [HttpPost]
            public async Task<ActionResult> ActivationDeactivationVendorActivate(int id)
            {
                var isActivated = await _vendorService.ActivateVendorAsync(id);

                if (isActivated)
                {
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
                else
                {
                    ViewBag.ErrorMessage = "Vendor activation failed.";
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
            }

            // GET: Vendor/Deactivate/5
            [HttpPost]
            public async Task<ActionResult> ActivationDeactivationVendorDeactivate(int id)
            {
                var isDeactivated = await _vendorService.DeactivateVendorAsync(id);

                if (isDeactivated)
                {
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
                else
                {
                    ViewBag.ErrorMessage = "Vendor deactivation failed.";
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
            }

            // Post: Vendor/Delete/5
            [HttpPost]
        [Route("ActivationDeactivationVendorDelete/{id}")]
        public async Task<ActionResult> ActivationDeactivationVendorDelete(int id)
            {
                var isDeleted = await _vendorService.DeleteVendorAsync(id);

                if (isDeleted)
                {
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
                else
                {
                    ViewBag.ErrorMessage = "Vendor deletion failed.";
                    return RedirectToAction("GetAllActivationDeactivationVendor");
                }
            }
  

}
}