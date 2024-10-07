using AppAPI.Repositories;
using AppData.DTO;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/user
        [HttpGet("get-all")]
        public IActionResult GetAllUsers()
        {
            var entity = _userRepository.GetAll();
            var dto = _mapper.Map<IEnumerable<WebUser>>(entity);
            return Ok(dto);
        }

        // GET api/user/get-by-id/{id}
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var entity = _userRepository.GetById(id);
            if (entity != null)
            {
                var dto = _mapper.Map<WebUser>(entity);
                return Ok(dto);
            }
            return NotFound();
        }

        // POST api/user/create
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] WebUser value)
        {
            try
            {
                value.UserId = Guid.NewGuid();
                value.CreatedAt = DateTime.UtcNow;
                value.UpdatedAt = DateTime.UtcNow;

                var mappedResult = _mapper.Map<User>(value);
                _userRepository.Add(mappedResult);
                return Ok(new { message = "Thêm người dùng thành công", value });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/user/update/{id}
        // PUT api/user/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] WebUser value)
        {
            // Lấy thông tin người dùng từ repository dựa trên id
            var entity = _userRepository.GetById(id);
            if (entity == null)
            {
                return NotFound("Người dùng không tìm thấy");
            }

            // Giữ nguyên UserId và cập nhật lại các thông tin khác từ value
            entity.FullName = value.FullName; // Cập nhật tên
            entity.Email = value.Email; // Cập nhật email
            entity.PhoneNumber = value.PhoneNumber; // Cập nhật số điện thoại
            entity.Address = value.Address; // Cập nhật địa chỉ
            entity.Password = value.Password; // Cập nhật mật khẩu (có thể cần mã hóa nếu cần thiết)
            entity.Status = value.Status; // Cập nhật trạng thái
            entity.UpdatedAt = DateTime.UtcNow; // Cập nhật lại thời gian chỉnh sửa
                                                // Bạn có thể bổ sung thêm các trường khác nếu cần

            // Thực hiện cập nhật thông tin vào database
            _userRepository.ModifileUpdate(entity);

            // Trả về kết quả cập nhật thành công
            return Ok(new { message = "Cập nhật người dùng thành công", value = entity });
        }


        // DELETE api/user/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("Người dùng không tìm thấy");
            }

            _userRepository.Remove(user);
            return Ok(new { message = "Xóa người dùng thành công" });
        }
    }
}
