using AppData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Enum;

namespace AppData.DTO
{
	public class FieldShiftDTO
	{
		[Key]
		public Guid IdFieldShift { get; set; }
		public Guid IdShift { get; set; }
		public Guid IdField { get; set; }
		public DateTime Time { get; set; }
		public FieldShiftStatus Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string? CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }

		// Bổ sung thuộc tính liên kết
		public string? FieldName { get; set; } // Tên sân
		public string? ShiftName { get; set; } // Tên ca
		public TimeSpan? StartTime { get; set; } // Thời gian bắt đầu
		public TimeSpan? EndTime { get; set; } // Thời gian kết thúc
		public decimal? Price { get; set; }
	}

}
