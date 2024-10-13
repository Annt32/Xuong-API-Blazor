using AppAPI.Repositories;
using AppData.DTO.FieldType_DTO;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FieldTypeController : ControllerBase
	{
        private readonly IRepository<FieldType> _repos;
        private readonly IMapper _mapper;
        public FieldTypeController(IRepository<FieldType> repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var lst = _repos.GetAll();
            return Ok(lst.Select(x => _mapper.Map<FieldTypeDTO>(x)).ToList());
        }

        [HttpGet("get-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entity = _repos.GetById(id);
                if (entity == null)
                {
                    return NotFound("Không tìm thấy loại sân bóng với ID này");
                }

                var fieldTypeDTO = _mapper.Map<FieldTypeDTO>(entity);
                return Ok(fieldTypeDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] FieldTypeCreateRequest input)
        {
            try
            {
                var entity = new FieldType();
                entity.Id = Guid.NewGuid();
                entity.Name = input.Name;
                entity.Price = input.Price;
                entity.Description = input.Description;

                entity.CreatedAt = DateTime.Now;
                _repos.Add(entity);

                return Ok("Thêm loại sân bóng thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
		public async Task<IActionResult> Update([FromBody] FieldTypeUpdateRequest input)
		{
			try
			{
				var entity = _repos.GetById(input.Id);

                if(entity == null)
                {
                    return BadRequest("Not found");
                }


				entity.Name = input.Name;
				entity.Price = input.Price;
                entity.Description = input.Description;

                entity.UpdatedAt = DateTime.Now;
				_repos.Update(entity);

				return Ok("Sửa loại sân bóng thành công");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteField(Guid id)
        {
            var field = _repos.GetById(id);
            if (field == null)
            {
                return NotFound("Field not found");
            }

            _repos.Remove(field);
            return Ok(new { message = "Xóa sân bóng thành công" });
        }
    }
}
