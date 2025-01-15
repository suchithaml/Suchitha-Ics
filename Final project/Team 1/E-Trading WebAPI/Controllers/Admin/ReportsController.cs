using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using E_Trading_WebAPI.Models;



namespace E_Trading_WebAPI.Controllers
{
    [RoutePrefix("api/report")]
    public class ReportsController : ApiController
    {
        private ETradingDBEntities context = new ETradingDBEntities();

        // GET: api/report/GetTotalSales
        [HttpGet]
        [Route("GetTotalSales")]
        public async Task<IHttpActionResult> GetTotalSales()
        {
            try
            {
                var result = await context.Database.SqlQuery<TotalSalesResult>("EXEC GetTotalSales").FirstOrDefaultAsync();

                if (result == null || result.TotalSales == 0)
                {
                    return Content(HttpStatusCode.NotFound, "No sales data available.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/report/GetTotalSalesByDate
        [HttpGet]
        [Route("GetTotalSalesByDate")]
        public async Task<IHttpActionResult> GetTotalSalesByDate(DateTime Date)
        {
            try
            {
                var DateParam = new SqlParameter("@Date", Date.ToString("yyyy-MM-dd"));
                var result = await context.Database.SqlQuery<TotalSalesResultByDate>("EXEC GetTotalSalesByDate @Date", DateParam).FirstOrDefaultAsync();

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "No sales data available on selected date.");
                }


                //foreach (var item in result)
                //{
                result.SalesDate = Date;
                //}

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/report/GetTopSellingProducts
        [HttpGet]
        [Route("GetTopSellingProducts")]
        public async Task<IHttpActionResult> GetTopSellingProducts()
        {
            try
            {
                var result = await context.Database.SqlQuery<TopSellingProductResult>("EXEC GetTopSellingProducts").ToListAsync();

                if (!result.Any())
                {
                    return Content(HttpStatusCode.NotFound, "No top-selling products available.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/report/GetTopSellingProductsByVendor/{VendorID}
        [HttpGet]
        [Route("GetTopSellingProductsByVendor/{VendorID}")]
        public async Task<IHttpActionResult> GetTopSellingProductsByVendor(int VendorID)
        {
            try
            {
                var vendorIdParam = new SqlParameter("@VendorID", VendorID);
                var result = await context.Database.SqlQuery<TopSellingProductByVendorResult>("EXEC GetTopSellingProductsByVendor @VendorID", vendorIdParam).ToListAsync();

                if (!result.Any())
                {
                    return Content(HttpStatusCode.NotFound, $"No top-selling products found for VendorID {VendorID}.");
                }
                foreach (var item in result)
                {
                    item.VendorID = VendorID;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetVendors")]
        public async Task<IHttpActionResult> GetVendors()
        {
            try
            {

                var result = await context.Database.SqlQuery<GetVendorsTopSellingProductsByVendor>("SELECT VendorID, VendorName FROM Vendor").ToListAsync();

                if (result == null || !result.Any())
                {
                    return Content(HttpStatusCode.NotFound, "No Vendors available.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

}
