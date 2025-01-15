using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class CheckoutService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custcheckout"; // Replace with your actual Web API URL

        // Method to process the checkout
        public async Task<ResponseModel> ProcessCheckoutAsync(CheckOutModel checkout)
        {
            // Check if checkout data is null
            if (checkout == null)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = "Invalid checkout data."
                };
            }

            using (var client = new HttpClient())
            {
                try
                {
                    // Serialize checkout data to JSON
                    var json = JsonConvert.SerializeObject(checkout);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Debugging: Log the request body
                    System.Diagnostics.Debug.WriteLine($"Sending Checkout Data: {json}");

                    // Send POST request to the API
                    var response = await client.PostAsync(_apiUrl, content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response
                        var resultJson = await response.Content.ReadAsStringAsync();

                        // Debugging: Log the response
                        System.Diagnostics.Debug.WriteLine($"API Response: {resultJson}");

                        return JsonConvert.DeserializeObject<ResponseModel>(resultJson);
                    }
                    else
                    {
                        // If the response is not successful, log the status code
                        System.Diagnostics.Debug.WriteLine($"API Request Failed with Status Code: {response.StatusCode}");
                        return new ResponseModel
                        {
                            Success = false,
                            Message = "Failed to process checkout."
                        };
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    return new ResponseModel
                    {
                        Success = false,
                        Message = "An error occurred while processing the checkout."
                    };
                }
            }
        }
    }
}