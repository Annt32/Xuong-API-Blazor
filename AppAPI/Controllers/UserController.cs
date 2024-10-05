using AppAPI.Repositories;
using AppData.DTO;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserController(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var entity = _userRepository.GetAll();
            var dto = _mapper.Map<IEnumerable<WebUser>>(entity);
            return Ok(dto);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = _userRepository.GetById(id);
            if (entity != null)
            {
                var dto = _mapper.Map<WebUser>(entity);
                return Ok(dto);
            }
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] WebUser value)
        {
            
            try
            {
                value.UserId = Guid.NewGuid();
                value.CreatedAt = DateTime.UtcNow;
                value.UpdatedAt = DateTime.UtcNow;

                var mappedresult = _mapper.Map<User>(value);
                _userRepository.Add(mappedresult);
                return Ok(new { message = "Thêm người dùng thành công", value });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public IActionResult Put([FromBody] WebUser value)
        {
            var mappedresult = _mapper.Map<User>(value);
            if (mappedresult == null)
            {
                return NotFound("Field not found");
            }

            _userRepository.ModifileUpdate(mappedresult);

            return Ok(new { message = "Cập nhật người dùng thành công", value });
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("Field not found");
            }

            _userRepository.Remove(user);
            return Ok(new { message = "Xóa người dùng" +
                " thành công" });
        }
    }
}
