using AppData.Entities;
using BlazorServer.IServices;
using BlazorServer.Models;
using System.Text.Json;

namespace BlazorServer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NotificationViewModel>> GetAllNotificationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NotificationViewModel>>("https://localhost:7143/api/Notifications");
            return response ?? new List<NotificationViewModel>();
        }


        public async Task<bool> MarkAsViewedAsync(Guid idNotification)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7143/api/Notifications/{idNotification}/mark-as-viewed", new { });
            return response.IsSuccessStatusCode;
        }
    }
}
