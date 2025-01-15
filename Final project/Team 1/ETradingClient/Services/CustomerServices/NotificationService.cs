using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ETradingClient.Models;

namespace ETradingClient.Services
{
    public class NotificationService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/custnotifications/notifications"; // Replace with your Web API URL

        public async Task<List<NotificationViewModel>> GetNotificationsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<NotificationViewModel>>(json);
                }
            }

            return null;
        }
    }
}