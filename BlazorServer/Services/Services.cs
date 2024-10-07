using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BlazorServer.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public Services(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateFieldAsync(T value)
        {
            string requestURL = "https://localhost:7143/api/Field/fields-post";
            var jsonContent = JsonConvert.SerializeObject(value);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFieldAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Field/fields-delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<T>> GetAllFieldsAsync()
        {
            string requestURL = "https://localhost:7143/api/Field/fields-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<T>>(response);
        }

        public async Task<T> GetFieldByIdAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/Field/fields-get-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<bool> ModifileUpdateFieldAsync(T value)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFieldAsync(Guid id, T updatedValue)
        {
            string requestURL = $"https://localhost:7143/api/Field/fields-put/{id}";
            var jsonContent = JsonConvert.SerializeObject(updatedValue);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }
    }
}
