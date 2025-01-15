using Newtonsoft.Json;
using ETradingClient.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ETradingClient.Services
{
    public class UserService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/users"; 

        public async Task<string> RegisterUserAsync(User user)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_apiUrl}/register", content);
                if (response.IsSuccessStatusCode)
                {
                    return "Registration successful.";
                }
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }
        }

        public async Task<string> LoginUserAsync(User user)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_apiUrl}/login", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }
        }

        // New Method: Get User By Username
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/byusername?username={username}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(json);
                }
            }

            return null;
        }


    }
}