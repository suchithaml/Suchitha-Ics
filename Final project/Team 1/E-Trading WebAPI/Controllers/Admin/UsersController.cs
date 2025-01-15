using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers.Admin
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
            private readonly ETradingDBEntities _context = new ETradingDBEntities();

            // POST: api/users/register
            [HttpPost]
            [Route("register")]
            public IHttpActionResult Register([FromBody] User user)
            {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            // Check if username already exists
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Add the new user to the Users table
            user.CreatedAt = DateTime.Now; // Ensure CreatedAt is set
            _context.Users.Add(user);
            _context.SaveChanges();

            // Fetch the newly created UserID
            var createdUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (createdUser == null)
            {
                return BadRequest("User creation failed.");
            }

            // Add initial balance to the TopUp table
            var topUp = new TopUp
            {
                UserID = createdUser.UserID,
                AccNo = Convert.ToInt32(GenerateAccountNumber()), // Generate a unique account number
                Balance = 20000.00m // Initial balance
            };

            _context.TopUps.Add(topUp);
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
            dynamic user;

            if (loginUser.IsAdmin)
            {
                user = _context.Users
                               .FirstOrDefault(u => u.Username == loginUser.Username
                                                    && u.Password == loginUser.Password
                                                    && u.IsAdmin);          
            }
            else if (loginUser.IsCustomer)
            {               
                user = _context.Users
                               .Where(u => u.Username == loginUser.Username
                                        && u.Password == loginUser.Password
                                        && u.IsCustomer)
                               .Join(_context.Customers,
                                     u => u.UserID,
                                     c => c.CustomerID,
                                     (u, c) => u) 
                               .FirstOrDefault();
            }
            else if (loginUser.IsVendor)
            {
              
                user = _context.Users
                               .Where(u => u.Username == loginUser.Username
                                        && u.Password == loginUser.Password
                                        && u.IsVendor && u.Status.ToLower().Equals("Approved"))
                               .Join(_context.Vendors,
                                     u => u.UserID,
                                     v => v.VendorID,
                                     (u, v) => u)  
                               .FirstOrDefault();
            }
            else
            {
                user = null;
            }



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

      
        // Method to generate a unique account number
        private long GenerateAccountNumber()
        {
            // Example: Generate a random 10-digit number
            var random = new Random();
            return long.Parse($"{random.Next(1000000000, int.MaxValue)}");
        }

    }
    }
