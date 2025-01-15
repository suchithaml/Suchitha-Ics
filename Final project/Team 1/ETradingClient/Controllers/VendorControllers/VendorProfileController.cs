using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;    
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    //[RoutePrefix("VendorProfile")]
    public class VendorProfileController : Controller
    {
        private readonly VendorProfileService _vendorProfileService = new VendorProfileService();// Assuming this service handles API calls

        // GET: Profile/Add
        public async Task<ActionResult> Add()
        {
            // Ensure the user is logged in by checking session
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = Convert.ToInt32(Session["UserID"].ToString());
            var userProfile = await _vendorProfileService.GetVendorProfileAsync(userId);
            if(userProfile != null)
            {
                return View(userProfile);
            }

            ViewBag.Username = Session["Username"].ToString();
            return View();  // Show empty form for adding or updating profile
        }

        // POST: Profile/Add
        [HttpPost]
        public async Task<ActionResult> Add(Vendor vendor)
        {
            // Ensure the user is logged in
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            
            // Assign the VendorID from Session (this will be automatically handled by the view if using HiddenFor)
            vendor.VendorID = (int)Session["UserID"];
            vendor.CustomerID = (int)Session["UserID"];
            vendor.VendorIsActive = true;

            if (ModelState.IsValid)
            {
                // Call Web API to add or update the profile
                var result = await _vendorProfileService.AddOrUpdateProfileAsync(vendor);

                if (result.Success)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("Login", "Account");  // Redirect to the product creation page after success
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            else
            {
                // If model state is not valid, display the error messages.
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();

                // Optionally log the error messages or display them for debugging
                foreach (var errorMessage in errorMessages)
                {
                    // Log or display the error message as needed
                    System.Diagnostics.Debug.WriteLine(errorMessage); // Example: log it to console or a log file
                }

                // You can add a generic error message if needed for UI
                ModelState.AddModelError("", "There were some validation errors.");
            }

            return View(vendor);  // Return the form with error messages if validation failed
        }




        // GET: Profile
        public async Task<ActionResult> Index()
        {
            // Ensure the user is logged in by checking session
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not logged in
            }

            int userId = (int)Session["UserID"]; // Get UserID from session

            // Fetch user profile using the service
            var userProfile = await _vendorProfileService.GetVendorProfileAsync(userId);

            if (userProfile == null)
            {
                ViewBag.ErrorMessage = "Profile not found.";
                return View();
            }

            return View(userProfile); // Pass the user profile to the view
        }
    }
}