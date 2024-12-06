using AppAPI.Repositories;
using AppData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IRepository<Notification> _notificationRepository;
        public NotificationsController(IRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpGet]
        public IActionResult GetAllNotifications()
        {
            var notifications = _notificationRepository.GetNotificationsWithDetails()
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new
                {
                    IdNotification = n.IdNotification,
                    Message = n.Message,
                    IsViewed = n.IsViewed,
                    CreatedAt = n.CreatedAt,
                    UpdatedAt = n.UpdatedAt,
                    FieldName = n.FieldShift?.Field?.FieldName ?? "Không xác định",
                    ShiftName = n.FieldShift?.Shift?.ShiftName ?? "Không xác định",
                    StartTime = n.FieldShift?.Shift?.StartTime ?? "Không xác định",
                    EndTime = n.FieldShift?.Shift?.EndTime ?? "Không xác định"
                })
                .ToList();

            return Ok(notifications);
        }



        [HttpPut("{id}/mark-as-viewed")]
        public IActionResult MarkAsViewed(Guid id)
        {
            var notification = _notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound("Thông báo không tồn tại.");
            }

            notification.IsViewed = true;
            notification.UpdatedAt = DateTime.Now;
            _notificationRepository.Update(notification);

            return Ok("Đánh dấu đã xem thành công.");
        }

    }
}
