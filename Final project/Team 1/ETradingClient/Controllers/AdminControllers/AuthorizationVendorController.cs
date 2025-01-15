using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class AuthorizationVendorController : Controller
    {
        private readonly AuthorizationVendorService _authVenService = new AuthorizationVendorService();

        [HttpGet]
        //"Get :AuthorizationVendor/GetPendingVendors"
        public async Task<ActionResult> GetPendingVendors()
        {
            var vendors = await _authVenService.GetAllPendingVendorsAsync();

            if (vendors == null || vendors.Count ==0)
            {
                ViewBag.ErrorMessage = "No Pending Authorization";
                return View(new List<mdlAuthorizationPendingVendors>());
            }

            return View(vendors);
        }

        [HttpPost]
        //"Get :AuthorizationVendor/UpdateAuthorizationStatus/{}"
        public async Task<ActionResult> UpdateAuthorizationStatus(int CurVendorID,string ApprovedStatus)
        {
            var vendors = await _authVenService.UpdateAuthorizationStatusAsync(CurVendorID, ApprovedStatus);

            if (vendors == false)
            {
                ViewBag.ErrorMessage = "Pending Authorization Failed";
            }

            return RedirectToAction("GetPendingVendors");
        }
    }
}