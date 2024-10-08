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
namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FieldShiftController : ControllerBase
	{
		private readonly IRepository<FieldShift> _fieldshiftRepository;
		private readonly IMapper _mapper;

		public FieldShiftController(IRepository<FieldShift> fieldshiftRepository, IMapper mapper)
		{
			_fieldshiftRepository = fieldshiftRepository;
			_mapper = mapper;
		}

		[HttpGet("fieldshift-get")]
		public IActionResult GetFieldshift()
		{
			var fieldshift = _fieldshiftRepository.GetAll();
			return Ok(fieldshift.Select(x => _mapper.Map<FieldShiftDTO>(x)).ToList());
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
		public IActionResult CreateField([FromBody] FieldShiftDTO fieldshift)
		{
			try
			{
				var entity = new FieldShift();
				entity.IdFieldShift = Guid.NewGuid();
				entity.IdShift = fieldshift.IdShift;
				entity.IdFieldDetail = fieldshift.IdFieldDetail;
				entity.Time = fieldshift.Time;
				entity.Status = fieldshift.Status;
				entity.CreatedAt = DateTime.UtcNow;
				entity.UpdatedAt = DateTime.UtcNow;

				_fieldshiftRepository.Add(entity);
				return Ok(new { message = "Thêm sân bóng thành công", entity });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPut("fieldshift-put/{id}")]
		public IActionResult UpdateField(Guid id, [FromBody] FieldShiftDTO fieldshiftUpdate)
		{
			try
			{
				var entity = _fieldshiftRepository.GetById(fieldshiftUpdate.IdFieldShift);
				if (entity == null)
				{
					return BadRequest();
				}
				entity.Time = fieldshiftUpdate.Time;
				entity.Status = fieldshiftUpdate.Status;
				entity.CreatedAt = DateTime.UtcNow;
				entity.UpdatedAt = DateTime.UtcNow;

				_fieldshiftRepository.Update(entity);
				return Ok("Sua Ca San Thanh Cong");
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpDelete("fieldshift-delete/{id}")]
		public async Task<IActionResult> Delete([FromBody] FieldShiftDTO input,Guid id)
		{
			try
			{
				AppDBContext _context = new AppDBContext();
				var entity = _fieldshiftRepository.GetById(id);
				var check = _context.Shifts.FirstOrDefault(x => x.IdShift == entity.IdShift);
				if (entity == null)
				{
					return BadRequest("Not found");
				}
				if (check != null)
				{
					return BadRequest("Cannot Remove Because Some Reason");
				}
				_fieldshiftRepository.Remove(entity);

				return Ok("Xóa thành công ");
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}");
			}
		}
	}
}
