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

        // Thêm người dùng mới
        public async Task<bool> CreateFieldAsync(WebUser value)
        {
            string requestURL = "https://localhost:7143/api/User/create";
            var jsonContent = JsonConvert.SerializeObject(value);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        // Xóa người dùng dựa trên ID
        public async Task<bool> DeleteFieldAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/User/delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }

        // Lấy tất cả người dùng
        public async Task<List<WebUser>> GetAllFieldsAsync()
        {
            string requestURL = "https://localhost:7143/api/User/get-all";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<WebUser>>(response);
        }

        // Lấy người dùng bằng ID
        public async Task<WebUser> GetFieldByIdAsync(Guid id)
        {
            string requestURL = $"https://localhost:7143/api/User/get-by-id/{id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<WebUser>(response);
        }

        // Cập nhật người dùng (với toàn bộ thông tin)
        public async Task<bool> ModifileUpdateFieldAsync(WebUser value)
        {
            string requestURL = "https://localhost:7143/api/User/update";
            var jsonContent = JsonConvert.SerializeObject(value);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);
            return response.IsSuccessStatusCode;
        }

        // Cập nhật người dùng dựa trên ID
        public async Task<bool> UpdateFieldAsync(Guid id, WebUser updatedValue)
        {
            string requestURL = $"https://localhost:7143/api/User/update/{id}";

            var jsonContent = JsonConvert.SerializeObject(updatedValue);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestURL, content);

            return response.IsSuccessStatusCode;
        }
    }
}
