using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindApp.Models;


namespace NorthwindApp.Controllers
{
    public class CodeController : Controller
    {
         private NorthwindEntities db = new NorthwindEntities();
    // Action Method 1: Customers in Germany
    public ActionResult CustomersInGermany()
    {
        var customers = db.Customers.Where(c => c.Country == "Germany").ToList();
        return View(customers);
    }

    // Action Method 2: Customer details with OrderId 10248
    public ActionResult CustomerByOrderId()
    {
        var order = db.Orders
                      .Where(o => o.OrderID == 10248)
                      .Select(o => new
                      {
                          o.Customer.ContactName,
                          o.Customer.CompanyName,
                          o.Customer.Address,
                          o.Customer.City,
                          o.Customer.Country
                      }).FirstOrDefault();

        return View(order);
    }
    }
}