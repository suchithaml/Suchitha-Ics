using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class ProfileService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custprofile"; // Replace with your API URL

        // Get customer profile by UserID
        public async Task<Customer> GetCustomerProfileAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/profile/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Customer>(json);
                }

                return null;
            }
        }

        // Add or update user profile
        public async Task<(bool Success, string Message)> AddOrUpdateProfileAsync(Customer customer)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(
                        $"{_apiUrl}/add",
                        new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json")
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