using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;
using System.Net;

namespace ETradingClient.Services
{
    public class ReportService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/report";

        // GET: api/report/GetTotalSales
        public async Task<mdlTotalSalesResult> GetTotalSalesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetTotalSales");
                if (response.IsSuccessStatusCode)
                {
                    var totalSalesJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<mdlTotalSalesResult>(totalSalesJson);
                }

                return null;  // No data or an error response
            }
        }

        // GET: api/report/GetTotalSalesByDate
        public async Task<mdlTotalSalesResultByDate> GetTotalSalesByDateAsync(DateTime date)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetTotalSalesByDate?Date={date.ToString("yyyy-MM-dd")}");
                if (response.IsSuccessStatusCode)
                {
                    var salesByDateJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<mdlTotalSalesResultByDate>(salesByDateJson);
                }

                return  new mdlTotalSalesResultByDate() { TotalSales = 0, SalesDate = date } ;
                
            }
        }

        // GET: api/report/GetTopSellingProducts
        public async Task<List<mdlTopSellingProductResult>> GetTopSellingProductsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetTopSellingProducts");
                if (response.IsSuccessStatusCode)
                {
                    var topSellingProductsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<mdlTopSellingProductResult>>(topSellingProductsJson);
                }

                return new List<mdlTopSellingProductResult>();  // Return an empty list if no data found
            }
        }

        // GET: api/report/GetTopSellingProductsByVendor/{VendorID}
        public async Task<List<mdlReportProductDetails>> GetTopSellingProductsByVendorAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetTopSellingProductsByVendor/{vendorId}");
                if (response.IsSuccessStatusCode)
                {
                    var topSellingProductsByVendorJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<mdlReportProductDetails>>(topSellingProductsByVendorJson);
                }

                return new List<mdlReportProductDetails>();  // Return an empty list if no data found
            }
        }

        // GET: api/report/GetVendorsAsync
        public async Task<List<mdlGetVendorsTopSellingProductsByVendor>> GetVendorsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetVendors");
                if (response.IsSuccessStatusCode)
                {
                    var salesByDateJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<mdlGetVendorsTopSellingProductsByVendor>>(salesByDateJson);
                }

                return new List<mdlGetVendorsTopSellingProductsByVendor> {} ;

            }
        }
    }
}
