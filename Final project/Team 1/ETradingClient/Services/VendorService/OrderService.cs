using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using ETradingClient.Models;
using Newtonsoft.Json;

namespace ETradingClient.Services
{
    public class VendorOrderService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/orderdetails/vendor"; // Replace with your API URL

        // Fetch orders by UserID
        public async System.Threading.Tasks.Task<List<OrderwithDetails>> GetOrdersByUserIdAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // API call to get orders by UserID
                    var response = await client.GetAsync($"{_apiUrl}/{vendorId}");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and deserialize the response to a list of OrderWithDetails objects
                        var json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<OrderwithDetails>>(json);
                    }

                    throw new Exception("Failed to retrieve orders for the specified user.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occurred: {ex.Message}");
                }
            }
        }
    }
}