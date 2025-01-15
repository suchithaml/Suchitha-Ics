using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class VendorProductService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/product"; // Your Web API URL



        // Method to get all products
        public async Task<List<Product>> GetProductsAsync(int vendorId)
        {
            
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/show/{vendorId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Product>>(json);
                    }

                    throw new Exception("Failed to retrieve products.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occurred: {ex.Message}");
                }
            }
        }

        // Method to get a product by ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{_apiUrl}/{id}");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(json); // Deserialize as a single product

                    return product;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unexpected error: {ex.Message}");
                }
            }
        }

        public async Task<string> UpdateProductAsync(int id, Product product)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{_apiUrl}/update/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return "Product updated successfully.";
                    }

                    var error = await response.Content.ReadAsStringAsync();
                    return $"Failed to update product: {error}";
                }
                catch (Exception ex)
                {
                    return $"Error occurred: {ex.Message}";
                }
            }
        }



        // Method to add a new product
        public async Task<string> AddProductAsync(Product product)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{_apiUrl}/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return "Product added successfully.";
                    }

                    var error = await response.Content.ReadAsStringAsync();
                    return $"Failed to add product: {error}";
                }
                catch (Exception ex)
                {
                    return $"Error occurred: {ex.Message}";
                }
            }
        }

        // Method to delete a product by ID
        public async Task<string> DeleteProductAsync(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync($"{_apiUrl}/delete/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize response into a strongly-typed object
                        var result = JsonConvert.DeserializeObject<DeleteResponse>(jsonResponse);

                        return result?.Message ?? "Product deleted successfully.";
                    }
                    else
                    {
                        var errorDetails = await response.Content.ReadAsStringAsync();
                        return $"Failed to delete product. API responded with status: {response.StatusCode}. Details: {errorDetails}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    return $"API Request failed: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Unexpected error: {ex.Message}";
                }
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/VendorProductAddShowCategories");
                if (response.IsSuccessStatusCode)
                {
                    var categoriesJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Category>>(categoriesJson);
                }

                return new List<Category>();
            }
        }

        // Helper class for deserialization
        public class DeleteResponse
        {
            public string Message { get; set; }
        }

    }
}