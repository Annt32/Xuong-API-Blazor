using AppData.DTO.User_RoleDto;
using AppData.Entities;
using BlazorServer.IServices;
using Newtonsoft.Json;
using System.Text;

namespace BlazorServer.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        //public UserService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}
        public UserService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        // Thêm người dùng mới
        public async Task<bool> CreateUsersAsync(WebUser value)
        {
            string requestURL = "api/User/create";

            var response = await _httpClient.PostAsJsonAsync(requestURL, value);
            return response.IsSuccessStatusCode;
        }

        // Xóa người dùng dựa trên ID
        public async Task<bool> DeleteUsersAsync(Guid id)
        {
            string requestURL = $"api/User/delete/{id}";
            var response = await _httpClient.DeleteAsync(requestURL);
            return response.IsSuccessStatusCode;
        }

        // Lấy tất cả người dùng
        public async Task<List<WebUser>> GetAllUsersAsync()
        {
            string requestURL = "api/User/get-all";
            var response = await _httpClient.GetFromJsonAsync<List<WebUser>>(requestURL);
            return response;
        }

        // Lấy người dùng bằng ID
        public async Task<WebUser> GetUsersByIdAsync(Guid id)
        {
            string requestURL = $"api/User/get-by-id/{id}";
            var response = await _httpClient.GetFromJsonAsync<WebUser>(requestURL);
            return response;
        }

        // Cập nhật người dùng (với toàn bộ thông tin)
        public async Task<bool> ModifileUpdateUsersAsync(WebUser value)
        {
            string requestURL = "api/User/update";

            var response = await _httpClient.PutAsJsonAsync(requestURL, value);
            return response.IsSuccessStatusCode;
        }

        // Cập nhật người dùng dựa trên ID
        public async Task<bool> UpdateUsersAsync(Guid id, WebUser updatedValue)
        {
            string requestURL = $"api/User/update/{id}";


            var response = await _httpClient.PutAsJsonAsync(requestURL, updatedValue);

            return response.IsSuccessStatusCode;
        }
    }
}
