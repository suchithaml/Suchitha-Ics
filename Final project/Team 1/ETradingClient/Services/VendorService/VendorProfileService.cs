

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class VendorProfileService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/vendorProfile"; // Replace with your Web API URL

    
        public async Task<Vendor> GetVendorProfileAsync(int vendorId)
        {
            if (vendorId <= 0)
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/{vendorId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Vendor>(json);
                }
            }

            return null;
        }


        public async Task<(bool Success, string Message)> AddOrUpdateProfileAsync(Vendor vendor)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(
                        $"{_apiUrl}/add",
                        new StringContent(JsonConvert.SerializeObject(vendor), Encoding.UTF8, "application/json")
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                        return (true, result.Message);
                    }
                    else
                    {
                        return (false, "Failed to add/update profile.");
                    }
                }
                catch
                {
                    return (false, "An error occurred while processing your request.");
                }
            }
        }
    }
}