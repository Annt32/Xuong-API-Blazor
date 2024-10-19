using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.Repositories;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IRepository<Shift> _shiftRepository;

        public ShiftController(IRepository<Shift> shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        // GET: api/Shift/shifts-get
        [HttpGet("shifts-get")]
        public IActionResult GetShifts()
        {
            var shifts = _shiftRepository.AsQueryable().ToList();
            return Ok(shifts);
        }

        // GET: api/Shift/shifts-get-id/{id}
        [HttpGet("shifts-get-id/{id}")]
        public IActionResult GetShiftById(Guid id)
        {
            var shift = _shiftRepository.GetById(id);
            if (shift == null)
            {
                return NotFound("Shift not found");
            }
            return Ok(shift);
        }

        // POST: api/Shift/shifts-post
        [HttpPost("shifts-post")]
        public IActionResult CreateShift(Shift shift)
        {
            try
            {
                shift.IdShift = Guid.NewGuid(); // Tạo GUID mới cho Shift
                shift.CreatedAt = DateTime.UtcNow;
                shift.UpdatedAt = DateTime.UtcNow;

                _shiftRepository.Add(shift);
                return Ok(new { message = "Thêm ca làm việc thành công", shift });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Shift/shifts-put/{id}
        [HttpPut("shifts-put/{id}")]
        public IActionResult UpdateShift(Guid id, Shift shiftUpdate)
        {
            var shift = _shiftRepository.GetById(id);
            if (shift == null)
            {
                return NotFound("Shift not found");
            }

            // Cập nhật các trường cần thiết
            shift.ShiftName = shiftUpdate.ShiftName;
            shift.StartTime = shiftUpdate.StartTime;
            shift.EndTime = shiftUpdate.EndTime;
            shift.Status = shiftUpdate.Status;
            shift.UpdatedAt = DateTime.UtcNow;
            shift.UpdatedBy = shiftUpdate.UpdatedBy;

            _shiftRepository.Update(shift);
            return Ok(new { message = "Cập nhật ca làm việc thành công", shift });
        }

        // DELETE: api/Shift/shifts-delete/{id}
        [HttpDelete("shifts-delete/{id}")]
        public IActionResult DeleteShift(Guid id)
        {
            var shift = _shiftRepository.GetById(id);
            if (shift == null)
            {
                return NotFound("Shift not found");
            }

            _shiftRepository.Remove(shift);
            return Ok(new { message = "Xóa ca làm việc thành công" });
        }
    }
}
