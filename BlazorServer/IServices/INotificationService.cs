using AppData.Entities;
using BlazorServer.Models;

namespace BlazorServer.IServices
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetAllNotificationsAsync();
        Task<bool> MarkAsViewedAsync(Guid idNotification);
    }
}

