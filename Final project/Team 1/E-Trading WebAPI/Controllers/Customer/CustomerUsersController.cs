using System;
using System.Linq;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers.Customer
{
    [RoutePrefix("api/custusers")]
    public class CustomerUsersController : ApiController
    {
        private readonly ETradingDBEntities1 _context = new ETradingDBEntities1();

        // POST: api/users/register
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists.");
            }

            user.CreatedAt = DateTime.Now; // Ensure CreatedAt is set
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

        // POST: api/users/login
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] User loginUser)
        {
            if (loginUser == null || string.IsNullOrWhiteSpace(loginUser.Username) || string.IsNullOrWhiteSpace(loginUser.Password))
            {
                return BadRequest("Invalid login data.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Message = "Login successful.",
                UserID = user.UserID,
                Role = user.IsAdmin ? "Admin" : user.IsVendor ? "Vendor" : "Customer"
            });
        }

        // GET: api/users/byusername?username={username}
        [HttpGet]
        [Route("byusername")]
        public IHttpActionResult GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Invalid username.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                UserID = user.UserID,
                Username = user.Username,
                Role = user.IsAdmin ? "Admin" : user.IsVendor ? "Vendor" : "Customer"
            });
        }

        [HttpGet]
        [Route("balance/{userId}")]
        public IHttpActionResult GetUserBalance(int userId)
        {
            // Calculate the total balance for the given UserID
            var totalBalance = _context.TopUps
                .Where(t => t.UserID == userId)
                .Sum(t => (decimal?)t.Balance) ?? 0;

            return Ok(totalBalance);
        }
    }
}
