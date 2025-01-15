using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETradingClient.Models;
using Newtonsoft.Json;

namespace ETradingClient.Services
{
    public class AuthorizationVendorService {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/Authorization";

        public async Task<List<mdlAuthorizationPendingVendors>> GetAllPendingVendorsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetPendingVendors");
                if (response.IsSuccessStatusCode)
                {
                    var vendorsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<mdlAuthorizationPendingVendors>>(vendorsJson);
                }
                return new List<mdlAuthorizationPendingVendors>();
            }
        }

        public async Task<bool> UpdateAuthorizationStatusAsync(int UserID, string status)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = $"{_apiUrl}/UpdateAuthorizationStatus?UserID={UserID}&status={status}";

                try
                {
                    var response = await client.PutAsync(apiUrl, null); 
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
 
                    return false;
                }
            }
        }


    }
}