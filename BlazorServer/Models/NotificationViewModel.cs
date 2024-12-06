namespace BlazorServer.Models
{
    public class NotificationViewModel
    {
        public Guid IdNotification { get; set; }
        public string Message { get; set; }
        public bool IsViewed { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FieldName { get; set; } // Tên sân
        public string ShiftName { get; set; } // Tên ca
        public string StartTime { get; set; } // Thời gian bắt đầu
        public string EndTime { get; set; } // Thời gian kết thúc
    }

}
