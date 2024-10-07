using AppData.DTO;
using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using System.Text;

namespace BlazorServer.Services
{
    public class UserService : IServices<WebUser>
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateFieldAsync(WebUser value)
        {
            string requestURL = "https://localhost:7143/api/User";
            var jsonContent = JsonConvert.SerializeObject(value);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFieldAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/User/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<WebUser>> GetAllFieldsAsync()
        {
            string requestURL = "https://localhost:7143/api/User";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<WebUser>>(response);
        }

        public async Task<WebUser> GetFieldByIdAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/User/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<WebUser>(response);
        }

        public async Task<bool> ModifileUpdateFieldAsync(WebUser value)
        {
            string requestURL = $"https://localhost:7143/api/User";
            var jsonContent = JsonConvert.SerializeObject(value);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFieldAsync(Guid id, WebUser updatedValue)
        {
           

            throw new NotImplementedException();
        }
    }
}
