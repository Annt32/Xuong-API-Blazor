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

        [HttpPut("update")]
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] FieldTypeDelRequest input)
        {
            try
            {
                var entity = _repos.GetById(input.Id);

                if(entity == null)
                {
                    return BadRequest("Not found");
                }

                _repos.Remove(entity);

                return Ok("Xóa thành công " + entity.Name);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }
	}
}
