using Appdata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.Repositories;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IRepository<Field> _fieldRepository;

        public FieldController(IRepository<Field> fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        [HttpGet("fields-get")]
        public IActionResult GetFields()
        {
            var fields = _fieldRepository.GetAll();
            return Ok(fields);
        }

        [HttpGet("fields-get-id/{id}")]
        public IActionResult GetFieldById(Guid id)
        {
            var field = _fieldRepository.GetById(id);
            if (field == null)
            {
                return NotFound("Field not found");
            }
            return Ok(field);
        }

        [HttpPost("fields-post")]
        public IActionResult CreateField(Field field)
        {
            try
            {
                field.IdField = Guid.NewGuid();
                field.CreatedAt = DateTime.UtcNow;
                field.UpdatedAt = DateTime.UtcNow;

                _fieldRepository.Add(field);
                return Ok(new { message = "Thêm sân bóng thành công", field });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("fields-put/{id}")]
        public IActionResult UpdateField(Guid id, Field fieldUpdate)
        {
            var field = _fieldRepository.GetById(id);
            if (field == null)
            {
                return NotFound("Field not found");
            }

            field.FieldName = fieldUpdate.FieldName;
            field.Price = fieldUpdate.Price;
            field.Description = fieldUpdate.Description;
            field.Status = fieldUpdate.Status;
            field.UpdatedAt = DateTime.UtcNow;
            field.UpdatedBy = fieldUpdate.UpdatedBy;

            _fieldRepository.Update(field);
            return Ok(new { message = "Cập nhật sân bóng thành công", field });
        }

        [HttpDelete("fields-delete/{id}")]
        public IActionResult DeleteField(Guid id)
        {
            var field = _fieldRepository.GetById(id);
            if (field == null)
            {
                return NotFound("Field not found");
            }

            _fieldRepository.Remove(field);
            return Ok(new { message = "Xóa sân bóng thành công" });
        }
    }
}
