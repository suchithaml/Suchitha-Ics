using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class OrderService
    {
        private readonly string _ordersApiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custorders"; // API endpoint for orders
        private readonly string _usersApiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custusers";  // API endpoint for users

        public async Task<decimal> GetUserBalanceAsync(int customerId)
        {
            using (var client = new HttpClient())
            {
                // Use the updated endpoint for user balance
                var response = await client.GetAsync($"{_usersApiUrl}/balance/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var balance = await response.Content.ReadAsStringAsync();
                    return decimal.Parse(balance);
                }
                return 0; // Return 0 if unable to fetch balance
            }
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int customerId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_ordersApiUrl}/cart/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var cartItems = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CartItem>>(cartItems);
                }
                return new List<CartItem>();
            }
        }

        public async Task<int> PlaceOrderAsync(int customerId, List<CartItem> cart)
        {
            using (var client = new HttpClient())
            {
                var orderData = new { CustomerId = customerId, Cart = cart };
                var content = new StringContent(JsonConvert.SerializeObject(orderData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_ordersApiUrl}/process", content);

                Console.WriteLine($"Response: {response.StatusCode}");
                Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return int.Parse(result);
                }
                return 0;
            }
        }

        public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_ordersApiUrl}/details/{orderId}");
                if (response.IsSuccessStatusCode)
                {
                    var orderDetails = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OrderDetailsViewModel>(orderDetails);
                }
                return null;
            }
        }

        public async Task<List<OrderHistoryViewModel>> GetOrderHistoryAsync(int customerId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_ordersApiUrl}/history/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<OrderHistoryViewModel>>(json);
                }
            }
            return null;
        }
    }
}