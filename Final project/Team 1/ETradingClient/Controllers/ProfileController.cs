using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models; // Assuming the Customer model is here
using ETradingClient.Services; // Assuming ProfileService is here

namespace ETradingClient.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService = new ProfileService();

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            if (Session["UserID"] == null) // Ensure user is logged in
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = (int)Session["UserID"]; // Get UserID from session
            var userProfile = await _profileService.GetCustomerProfileAsync(userId);

            if (userProfile == null) // If profile doesn't exist, redirect to Add
            {
                return RedirectToAction("Add");
            }

            return View(userProfile); // Show profile details
        }

        // GET: Profile/Add
        public ActionResult Add()
        {
            if (Session["UserID"] == null) // Ensure user is logged in
            {
                return RedirectToAction("Login", "Account");
            }

            if (Session["Role"].ToString().ToLower().Equals("vendor"))
            {
                return RedirectToAction("Add", "VendorProfile");
            }

                return View(new Customer()); // Display empty form
        }

        // POST: Profile/Add
        [HttpPost]
        public async Task<ActionResult> Add(Customer customer)
        {
            if (Session["UserID"] == null) // Ensure user is logged in
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(customer); // Return form with validation errors
            }

            customer.CustomerID = (int)Session["UserID"]; // Set CustomerID from session
            var result = await _profileService.AddOrUpdateProfileAsync(customer);

            if (result.Success)
            {
                ViewBag.Message = "Profile created successfully.";
                return RedirectToAction("Login", "Account"); // Redirect to login view after success
            }

            ModelState.AddModelError("", result.Message); // Display error
            return View(customer);
        }

        // POST: Profile/Update
        [HttpPost]
        public async Task<ActionResult> Update(Customer customer)
        {
            if (Session["UserID"] == null) // Ensure user is logged in
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", customer); // Return form with validation errors
            }

            customer.CustomerID = (int)Session["UserID"]; // Set CustomerID from session
            var result = await _profileService.AddOrUpdateProfileAsync(customer);

            if (result.Success)
            {
                ViewBag.Message = "Profile updated successfully.";
                return RedirectToAction("Index"); // Refresh the profile view
            }

            ModelState.AddModelError("", result.Message); // Display error
            return View("Index", customer);
        }
    }
}