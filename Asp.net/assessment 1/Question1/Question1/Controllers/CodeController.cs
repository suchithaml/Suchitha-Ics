

using Question1.Controllers;
using Question1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Question1.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities1 db = new NorthwindEntities1();

        // CustomersInGermany
        public ActionResult CustomersInGermany()
        {
            var customers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(customers);
        }

        //CustomerDetails
        public ActionResult CustomerDetails(int orderId)
        {
            var customer = db.Orders
                .Where(o => o.OrderID == orderId)
                .Select(o => o.Customer)
                .FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }


    }
}
