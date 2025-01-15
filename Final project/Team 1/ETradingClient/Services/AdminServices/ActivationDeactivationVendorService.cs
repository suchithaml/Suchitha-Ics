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
    public class ActivationDeactivationVendorService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/ActivationDeactivationVendor";

        // Get all active vendors
        public async Task<List<mdlActivationDeactivationVendor>> GetAllVendorsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}");
                if (response.IsSuccessStatusCode)
                {
                    var vendorsJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<mdlActivationDeactivationVendor>>(vendorsJson);
                }
                return new List<mdlActivationDeactivationVendor>();
            }
        }

        // Get a specific vendor by ID
        public async Task<mdlActivationDeactivationVendor> GetVendorDetailsAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/GetVendorDetails/{vendorId}");
                if (response.IsSuccessStatusCode)
                {
                    var vendorJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<mdlActivationDeactivationVendor>(vendorJson);
                }
                return null;  // Or handle appropriately
            }
        }

        // Activate a vendor by ID
        public async Task<bool> ActivateVendorAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync($"{_apiUrl}/Activate/{vendorId}", null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        // Deactivate a vendor by ID
        public async Task<bool> DeactivateVendorAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync($"{_apiUrl}/Deactivate/{vendorId}", null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        // Update a vendor's details
        public async Task<bool> UpdateVendorAsync(mdlActivationDeactivationVendor vendor)
        {
            using (var client = new HttpClient())
            {
                var vendorJson = JsonConvert.SerializeObject(vendor);
                var content = new StringContent(vendorJson, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_apiUrl}/Update", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        // Delete a vendor (set VendorIsActive to false)
        public async Task<bool> DeleteVendorAsync(int vendorId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{_apiUrl}/{vendorId}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}