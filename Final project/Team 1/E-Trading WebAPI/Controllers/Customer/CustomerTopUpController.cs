using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Trading_WebAPI.Models;

namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/custtopup")]
    public class CustomerTopUpController : ApiController
    {
        private readonly ETradingDBEntities _context;

        public CustomerTopUpController()
        {
            _context = new ETradingDBEntities();
        }

        // GET: api/topup/balance/{userId}
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

        // POST: api/topup/add
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddBalance([FromBody] TopUpRequest request)
        {
            if (request == null || request.UserID <= 0 || request.Amount <= 0)
            {
                return BadRequest("Invalid input data.");
            }

            var topUp = _context.TopUps.FirstOrDefault(t => t.UserID == request.UserID);
            if (topUp == null)
            {
                return NotFound(); // 404 if user not found
            }

            topUp.Balance += request.Amount; // Add balance
            _context.SaveChanges(); // Save changes to the database

            return Ok(new
            {
                Message = "Balance added successfully.",
                NewBalance = topUp.Balance
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
