using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ETradingClient.Models;
using System.Net;

namespace ETradingClient.Services
{
    public class CategoryService
    {
        private readonly string _apiUrl = $"https://localhost:{MvcApplication.PortNumber}/api/admincategories";

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_apiUrl}/ShowCategories");
                if (response.IsSuccessStatusCode)
                {
                    var categoriesJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Category>>(categoriesJson);
                }

                return new List<Category>();
            }
        }

        public async Task<Category> GetCategoryDetailsAsync(int categoryId)
        {
            using (var client = new HttpClient())
            {

                var response = await client.GetAsync($"{_apiUrl}/{categoryId}");
                if (response.IsSuccessStatusCode)
                {
                    var categoryJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Category>(categoryJson);
                }

                return new Category();
            }
        }


        public async Task<Category> CreateCategoryAsync(Category category)
        {
            using (var client = new HttpClient())
            {

                var categoryJson = JsonConvert.SerializeObject(category);
                var content = new StringContent(categoryJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{_apiUrl}/CreateCategory", content);
                if (response.IsSuccessStatusCode)
                {
                    var createdCategoryJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Category>(createdCategoryJson);
                }
                return new Category();
            }
        }


        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            using (var client = new HttpClient())
            {

                var categoryJson = JsonConvert.SerializeObject(category);
                var content = new StringContent(categoryJson, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{_apiUrl}/UpdateCategory", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;

            }
        }

        // Delete a category by ID
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            using (var client = new HttpClient())
            {

                var response = await client.DeleteAsync($"{_apiUrl}/DeleteCategory/{categoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }


    }
}