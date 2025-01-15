using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class TopUpService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custtopup"; // Replace with your actual API URL

        // Fetch the user's balance
        public async Task<decimal> GetUserBalanceAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/balance/{userId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var balance = await response.Content.ReadAsStringAsync();
                        return decimal.Parse(balance); // Return the balance as a decimal
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors here
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return 0;
        }

        // Add balance to the user's account
        public async Task<(bool Success, string Message)> AddBalanceAsync(TopUpRequest request)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    $"{_apiUrl}/add",
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    return (true, result.Message.ToString());
                }
                return (false, "Failed to add balance.");
            }
        }
    }
}