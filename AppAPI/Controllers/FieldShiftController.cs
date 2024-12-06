using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.Repositories;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using AppData.AppDbContext;
using AutoMapper;
using AppData.DTO;
using AppData.Enum;
namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FieldShiftController : ControllerBase
	{
		private readonly IRepository<FieldShift> _fieldshiftRepository;
		private readonly IRepository<Notification> _notificationRepository;
		private readonly IMapper _mapper;
		public List<DateTime> time;

		public FieldShiftController(IRepository<FieldShift> fieldshiftRepository, IMapper mapper, IRepository<Notification> notificationRepository)
		{
			_fieldshiftRepository = fieldshiftRepository;
			_mapper = mapper;
			_notificationRepository = notificationRepository;
		}

		[HttpGet("fieldshift-get")]
		public IActionResult GetFieldshift()
		{
			try
			{
				// Lấy dữ liệu từ FieldShift và các bảng liên quan
				var fieldshifts = _fieldshiftRepository
					.GetAllWithIncludes(fs => fs.Field, fs => fs.Shift)
					.ToList() // Chuyển đổi dữ liệu sang danh sách trước
					.Select(fs => new FieldShiftDTO
					{
						IdFieldShift = fs.IdFieldShift,
						IdField = fs.IdField,
						IdShift = fs.IdShift,
						Time = fs.Time,
						Status = fs.Status,
						CreatedAt = fs.CreatedAt,
						UpdatedAt = fs.UpdatedAt,
						CreatedBy = fs.CreatedBy,
						UpdatedBy = fs.UpdatedBy,
						FieldName = fs.Field?.FieldName,
						ShiftName = fs.Shift?.ShiftName,
						StartTime = TimeSpan.TryParse(fs.Shift?.StartTime, out var startTime) ? startTime : null,
						EndTime = TimeSpan.TryParse(fs.Shift?.EndTime, out var endTime) ? endTime : null
					})
					.ToList(); // Kết quả cuối cùng là danh sách

				return Ok(fieldshifts);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}



		[HttpGet("fieldshift-get-id/{id}")]
		public IActionResult GetFieldshiftById(Guid id)
		{
			var fieldshift = _fieldshiftRepository.GetById(id);
			if (fieldshift == null)
			{
				return NotFound("FieldShift not found");
			}
			return Ok(fieldshift);
		}

        [HttpPost("fieldshift-post")]
        public async Task<IActionResult> CreateField([FromBody] FieldShiftDTO fieldshift)
        {
            try
            {
                var newFieldShift = _mapper.Map<FieldShift>(fieldshift);
                _fieldshiftRepository.Add(newFieldShift);

                // Tạo thông báo
                var notification = new Notification
                {
                    IdFieldShift = newFieldShift.IdFieldShift,
                    Message = $"Sân vừa được đặt vào ngày {newFieldShift.Time:dd/MM/yyyy}.",
                    IsViewed = false,
                    CreatedBy = fieldshift.CreatedBy,
                    CreatedAt = DateTime.Now
                };
                _notificationRepository.Add(notification);

                // Trả về đối tượng FieldShiftDTO mới
                return Ok(_mapper.Map<FieldShiftDTO>(newFieldShift));
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                return BadRequest(new { message = $"Error: {innerException}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




        [HttpPut("fieldshift-put/{id}")]
        public async Task<IActionResult> UpdateField(Guid id, [FromBody] FieldShiftDTO fieldshiftUpdate)
        {
            try
            {
                var entity = _fieldshiftRepository.GetById(fieldshiftUpdate.IdFieldShift);
                if (entity == null)
                {
                    return BadRequest();
                }

                // Cập nhật thông tin FieldShift
                entity.Status = fieldshiftUpdate.Status;
                entity.IdShift = fieldshiftUpdate.IdShift;
                entity.IdField = fieldshiftUpdate.IdField;
                entity.CreatedBy = fieldshiftUpdate.CreatedBy;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = fieldshiftUpdate.UpdatedBy;
                _fieldshiftRepository.Update(entity);

                // Tạo thông báo
                var notification = new Notification
                {
                    IdFieldShift = entity.IdFieldShift,
                    Message = $"Lịch đặt sân đã được thay đổi vào ngày {DateTime.Now:dd/MM/yyyy}.",
                    IsViewed = false,
                    CreatedBy = fieldshiftUpdate.UpdatedBy,
                    CreatedAt = DateTime.Now
                };
                _notificationRepository.Add(notification);

                return Ok("Cập nhật lịch thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("fieldshift-delete/{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var entity = _fieldshiftRepository.GetById(id);
				if (entity == null)
				{
					return BadRequest("Not found");
				}

				// Xóa mềm: Cập nhật trạng thái thành "Cancel"
				entity.Status = FieldShiftStatus.Delete;
				_fieldshiftRepository.Update(entity);
				return Ok("Đã chuyển trạng thái thành 'Cancel'");
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}");
			}
		}





		[HttpPost]
		public async Task<IActionResult> CheckDate(DateTime check)
		{

            var list = _fieldshiftRepository.GetAll().OrderByDescending(t => t.Time).Take(60).ToList();
			
			foreach (var item in list)
			{
				 time.Add(item.Time);
			}


			
				for (int i = 0; i < 32; i++)
				{
				if (!time.Contains(check.AddDays(i)))
				{

					FieldShift fieldShift = new FieldShift();
					fieldShift.Time = check.AddDays(i);
					_fieldshiftRepository.Add(fieldShift);
				}
				break;
				}
				return Ok();
			
			
		}
	}
}
