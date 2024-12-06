using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DTO.Field_DTO
{
	public class FieldDTO
	{
		public Guid IdField { get; set; }
		public Guid FieldTypeId { get; set; }
		public string FieldName { get; set; }
		public string Location { get; set; }
		public int Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string? CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }
		public Guid IdFieldType { get; set; }
	}
}
