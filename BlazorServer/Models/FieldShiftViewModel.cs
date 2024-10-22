using AppData.Entities;
using AppData.Enum;

namespace BlazorServer.Models
{
    public class FieldShiftViewModel
    {
        public Guid IdFieldShift { get; set; }
        public Guid IdShift { get; set; }
        public Guid IdField { get; set; }
        public string ShiftName { get; set; } // Thông tin từ Shift
        public string FieldName { get; set; } // Thông tin từ Field
        public DateTime Time { get; set; } // Thông tin từ Field
        public string StartTime { get; set; } = "00:00:00";
        public string EndTime { get; set; } = "00:00:00";
        public FieldShiftStatus Status { get; set; }
        public bool IsCheckedIn { get; set; } // Trạng thái CheckIn (true/false)

        // Các thông tin khác từ Shift hoặc Field nếu cần
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Shift Shift { get; set; }
        public Field Field { get; set; }

        // Thông tin nhân viên và khách hàng
        public string EmployeeFullName { get; set; }  // Nhân viên
        public string EmployeePhoneNumber { get; set; }

        public string CustomerFullName { get; set; }  // Khách hàng
        public string CustomerPhoneNumber { get; set; }
    }

}
