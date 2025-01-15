using Newtonsoft.Json;
using ETradingClient.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ETradingClient.Services
{
    public class ProductService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custproducts"; // Replace [port] with your API port number

        // Fetch suggestions for product search
        public async Task<List<string>> GetSuggestionsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new List<string>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/search?query={query}");
                    if (response.IsSuccessStatusCode)
                    {
                        var suggestions = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<dynamic>>(suggestions)
                            .ConvertAll(s => (string)s.ProductName);
                    }
                }
                catch
                {
                    // Log or handle errors as needed
                }

                return new List<string>();
            }
        }

        // Fetch detailed product results
        public async Task<List<Product>> SearchProductsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new List<Product>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/search/details?query={query}");
                    if (response.IsSuccessStatusCode)
                    {
                        var products = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Product>>(products);
                    }
                }
                catch
                {
                    // Log or handle errors as needed
                }

                return new List<Product>();
            }
        }

        public async Task<Product> GetProductDetailsAsync(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/details/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var productJson = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Product>(productJson);
                    }
                }
                catch
                {
                    // Handle or log errors as needed
                }

                return null;  // Return null if product is not found or error occurred
            }
        }
        public async Task<(bool Success, string Message, int OrderId)> ProcessOrderAsync(OrderRequest orderRequest)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    $"{_apiUrl}/orders/process",
                    new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    if (result.success)
                    {
                        return (true, "Order placed successfully.", (int)result.orderId);
                    }
                    return (false, (string)result.message, -1);
                }
            }
            return (false, "Failed to process the order.", -1);
        }

        private readonly string _apiUrl1 = $"https://localhost:{MvcApplication.PortNumber}/api/profile"; // Replace with your API URL

        // Add or update user profile
        public async Task<(bool Success, string Message)> AddOrUpdateProfileAsync(Customer customer)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(
                        $"{_apiUrl1}/add",
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
        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return new List<Product>();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/byCategory?categoryName={categoryName}");
                    if (response.IsSuccessStatusCode)
                    {
                        var products = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Product>>(products);
                    }
                }
                catch
                {
                    // Log or handle errors as needed
                }

                return new List<Product>();
            }
        }

        public async Task<int?> GetVendorIdFromProductAsync(int productId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/vendor/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();
                    return result.VendorID;
                }
            }
            return null; // Return null if the API call fails or the vendor is not found
        }

        

   

    }
    }


    