using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETradingClient.Services;
using ETradingClient.Models;
using System.Threading.Tasks;

namespace ETradingClient.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ReportService _reportService = new ReportService();

        // GET: Reports/TotalSales
        public async Task<ActionResult> TotalSales()
        {
            var totalSales = await _reportService.GetTotalSalesAsync();

            if (totalSales == null)
            {
                ViewBag.ErrorMessage = "No total sales data available.";
            }

            return View(totalSales);
        }

        //[HttpGet]
        //[Route("ShowTotalSalesByDate")]
        //public ActionResult ShowTotalSalesByDate(mdlTotalSalesResultByDate mdlTotalSalesResultByDate)
        //{
        //    return View(mdlTotalSalesResultByDate);
        //}

        [HttpGet]
        public async Task<ActionResult> TotalSalesByDate()
        {

            var totalSalesByDate = await _reportService.GetTotalSalesByDateAsync(DateTime.Now);

            if (totalSalesByDate.TotalSales==0)
            {
                ViewBag.ErrorMessage = "No sales data found for the selected date.";
               // return View(totalSalesByDate);
                //return RedirectToAction("ShowTotalSalesByDate", new mdlTotalSalesResultByDate());
            }

            return View(totalSalesByDate);
        }


        // POST: Reports/TotalSalesByDate
        [HttpPost]
        public async Task<ActionResult> TotalSalesByDate(DateTime? txtdate)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
              .SelectMany(v => v.Errors)
              .Select(e => e.ErrorMessage)
              .ToList();

                ViewBag.ErrorMessage = string.Join("<br />", errorMessages);
                return View(new mdlTotalSalesResultByDate());
            }
            if (txtdate == null)
            {
                ViewBag.ErrorMessage ="Invalid Date format";
                return View(new mdlTotalSalesResultByDate());
            }
            DateTime parsedDate;

            if (!DateTime.TryParse(txtdate.ToString(), out parsedDate))
            {
                ViewBag.ErrorMessage = "Invalid date format. Please provide a valid date.";
                return View(new mdlTotalSalesResultByDate());
            }

            var totalSalesByDate = await _reportService.GetTotalSalesByDateAsync(parsedDate);

            if (totalSalesByDate.TotalSales==0)
            {
                ViewBag.ErrorMessage = "No sales data found for the selected date.";
               // RedirectToAction("ShowTotalSalesByDate", new mdlTotalSalesResultByDate());
            }

            return View(totalSalesByDate);
        }

        // GET: Report/TopSellingProducts
        public async Task<ActionResult> TopSellingProducts()
        {
            var topSellingProducts = await _reportService.GetTopSellingProductsAsync();

            if (!topSellingProducts.Any())
            {
                ViewBag.ErrorMessage = "No top-selling products available.";
            }

            return View(topSellingProducts);
        }

        // Get: Report/TopSellingProductsByVendor
        [HttpGet]
        public async Task<ActionResult> TopSellingProductsByVendor()
        {
            mdlTopSellingProductByVendorResult mdlTopSellingProductByVendorResult = new mdlTopSellingProductByVendorResult();
            mdlTopSellingProductByVendorResult.mdlGetVendorsTopSellingProductsByVendors=await _reportService.GetVendorsAsync();
            mdlTopSellingProductByVendorResult.mdlReportProductDetails = new List<mdlReportProductDetails>();
            return View(mdlTopSellingProductByVendorResult);
        }

        // POST: Report/TopSellingProductsByVendor
        [HttpPost]
        public async Task<ActionResult> TopSellingProductsByVendor(int vendorId)
        {
            mdlTopSellingProductByVendorResult mdlTopSellingProductByVendorResult = new mdlTopSellingProductByVendorResult();
            var topSellingProductsByVendor = await _reportService.GetTopSellingProductsByVendorAsync(vendorId);

            if (!topSellingProductsByVendor.Any())
            {
                ViewBag.ErrorMessage = "No top-selling products found for this vendor.";
                topSellingProductsByVendor=new List<mdlReportProductDetails>();
            }

            
            mdlTopSellingProductByVendorResult.mdlReportProductDetails = topSellingProductsByVendor;
            mdlTopSellingProductByVendorResult.mdlGetVendorsTopSellingProductsByVendors = await _reportService.GetVendorsAsync();

            return View(mdlTopSellingProductByVendorResult);
        }
    }
}