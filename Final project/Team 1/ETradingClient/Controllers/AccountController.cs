
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ETradingClient.Models;
using ETradingClient.Services;

namespace ETradingClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _service = new UserService();


        // GET: Account/Register
        public ActionResult Register()
        {
            User user = new User();
            user.IsCustomer = true;
            return View(user);
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage)
                              .ToList();

                ViewBag.ErrorMessage = string.Join("<br />", errorMessages);
                return View(model);
            }

            var result = await _service.RegisterUserAsync(model);
            if (result.Equals("Registration successful."))
            {
                //if (model.IsAdmin == true)
                //{
                //    return RedirectToRoute(new { controller = "Account", action = "Logout" });
                //}
                var user = await _service.GetUserByUsernameAsync(model.Username);
                if (user != null)
                {
                    Session["Username"] = user.Username;
                    Session["UserID"] = user.UserID;
                }
                if (model.IsVendor == true)
                {
                   return RedirectToRoute(new { controller = "VendorProfile", action = "Add" });
                }
                if (model.IsCustomer == true)
                {
                   return RedirectToRoute(new { controller = "Profile", action = "Add" });
                }
                 
            }

            ViewBag.Message = result;
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            User user = new User();
            user.IsCustomer = true;
            return View(user);
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(User model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
              .SelectMany(v => v.Errors)
              .Select(e => e.ErrorMessage)
              .ToList();

                ViewBag.ErrorMessage = string.Join("<br />", errorMessages);
                return View(model);
            }

            var result = await _service.LoginUserAsync(model);

            if (result.Contains("Login successful"))
            {
                var user = await _service.GetUserByUsernameAsync(model.Username);
                if (user != null)
                {
                    Session["Username"] = user.Username;
                    Session["UserID"] = user.UserID;
                    

                    Session["Role"] = user.Role;
                    if (model.IsAdmin == true)
                    {
                        return RedirectToRoute(new { controller = "Reports", action = "TotalSales" });
                    }
                    if (model.IsVendor == true)
                    {
                        return RedirectToRoute(new { controller = "VendorProduct", action = "Index" });
                    }
                    if (model.IsCustomer == true)
                    {
                        return RedirectToRoute(new { controller = "Products", action = "Search" });
                    }
                    
                }

                //return RedirectToAction("Search", "Products");
            }

            ViewBag.Message = result;
            return View();
        }
        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["UserID"] = null;


            Session["Role"] = null;
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}

