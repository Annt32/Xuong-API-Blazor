using AppAPI.Repositories;
using AppData.DTO.User_RoleDto;
using AppData.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;



        public UserController( UserManager<User> userManager = null, IMapper mapper = null)
        {
            _userManager = userManager;
            _mapper = mapper;
        }



        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WebUser>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<WebUser>> Get()
        {
            return Ok(_userManager.Users);
            //return Ok(/*_reviewRepo.Users*/_dbContext.Users.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WebUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WebUser>> Get(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            var dto = _mapper.Map<User>(result);
            if (id == null)
            {
                return NotFound();
            }
            else
                return Ok(dto);
        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] WebUser value, string roleName, string UserNow)
        {



            User user = new User()
            {
                UserName = value.UserName,
                Email = value.Email,
                FullName = value.FullName,
                Address = value.Address,
                Status = value.Status,
                PhoneNumber = value.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = UserNow
            };
            IdentityResult result = await _userManager.CreateAsync(user, value.Password);

            if (result.Succeeded)
            {
                if (roleName == "Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Staff"); // Thêm await ở đây
                    await _userManager.AddToRoleAsync(user, "Customer"); // Thêm await ở đây
                    IdentityResult resultRole = await _userManager.AddToRoleAsync(user, roleName);

                    if (resultRole.Succeeded)
                        return CreatedAtAction(nameof(Get), new { id = user.Id }, user.Id);
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa tạo thành công userRole:{string.Join(" ", resultRole.Errors.Select(e => e.Description))}");
                }
                else if (roleName == "Staff")
                {
                    await _userManager.AddToRoleAsync(user, "Customer"); // Thêm await ở đây
                    IdentityResult resultRole = await _userManager.AddToRoleAsync(user, roleName);

                    if (resultRole.Succeeded)
                        return CreatedAtAction(nameof(Get), new { id = user.Id }, user.Id);
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa tạo thành công userRole:{string.Join(" ", resultRole.Errors.Select(e => e.Description))}");
                }
                else
                {
                    IdentityResult resultRole = await _userManager.AddToRoleAsync(user, roleName);

                    if (resultRole.Succeeded)
                        return CreatedAtAction(nameof(Get), new { id = user.Id }, user.Id);
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa tạo thành công userRole:{string.Join(" ", resultRole.Errors.Select(e => e.Description))}");
                }
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa tạo thành công user:{string.Join(" ", result.Errors.Select(e => e.Description))}");




        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Edit([FromBody] WebUser value, string roleName)
        {
            var passwordHasher = new PasswordHasher<User>();
            var user1 = await _userManager.FindByNameAsync(value.UserName);
            if (user1 == null) { return NotFound(); }
            else
            {
                var id = user1.Id;
                user1.UserName = value.UserName;
                user1.PasswordHash = passwordHasher.HashPassword(user1, value.Password);
                user1.Email = value.Email;
                user1.PhoneNumber = value.PhoneNumber;
                user1.FullName = value.FullName;
                user1.Address = value.Address;
                user1.Status = value.Status;
                user1.UpdatedAt = DateTime.UtcNow;
                user1.UpdatedBy = "";
                var result = await _userManager.UpdateAsync(user1);
                IdentityResult addRoleResultStaff;
                IdentityResult addRoleResultAdmin;
                IdentityResult removeRoleResultAdmin;
                IdentityResult removeRoleResultStaff;
                if (!await _userManager.IsInRoleAsync(user1, roleName))
                {
                    if (roleName == "Admin")
                    {
                        addRoleResultStaff = await _userManager.AddToRoleAsync(user1, "Staff");
                        addRoleResultAdmin = await _userManager.AddToRoleAsync(user1, "Admin");
                        if (result.Succeeded)
                        {
                            if (addRoleResultAdmin.Succeeded)
                            {
                                if (addRoleResultStaff.Succeeded)
                                {
                                    return Ok("Sửa thành công admin nhưng chưa thành công thêm staff");
                                }

                                return Ok("Sửa thành công Admin");

                            }
                            else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa sửa thành công userRole:{string.Join(" ", addRoleResultAdmin.Errors.Select(e => e.Description))}");
                        }
                        else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa sửa thành công user:{string.Join(" ", result.Errors.Select(e => e.Description))}");

                    }
                    else if (roleName == "Staff")
                    {
                        removeRoleResultAdmin = await _userManager.RemoveFromRoleAsync(user1, "Admin");
                        addRoleResultStaff = await _userManager.AddToRoleAsync(user1, "Staff");
                        if (result.Succeeded)
                        {
                            if (addRoleResultStaff.Succeeded)
                            {
                                if (removeRoleResultAdmin.Succeeded)
                                {
                                    return Ok("Đã Xóa Khỏi Admin");
                                }
                                return Ok("Sửa thành công Staff, nhưng chưa xóa khỏi admin(nếu có)");
                            }
                            else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa sửa thành công userRole:{string.Join(" ", addRoleResultStaff.Errors.Select(e => e.Description))}");
                        }
                        else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa Sửa thành công user:{string.Join(" ", result.Errors.Select(e => e.Description))}");

                    }
                    else
                    {
                        removeRoleResultAdmin = await _userManager.RemoveFromRoleAsync(user1, "Admin");
                        removeRoleResultStaff = await _userManager.RemoveFromRoleAsync(user1, "Staff");
                        if (result.Succeeded)
                        {
                            if (removeRoleResultStaff.Succeeded)
                            {
                                if (removeRoleResultAdmin.Succeeded)
                                {
                                    return Ok("Đã Sửa thành Customer");
                                }
                                return Ok("Sửa thành Staff, nhưng chưa xóa khỏi Admin(Nếu có)");
                            }
                            else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa sửa thành công userRole:{string.Join(" ", removeRoleResultStaff.Errors.Select(e => e.Description))}");
                        }
                        else return StatusCode(StatusCodes.Status500InternalServerError, $"Chưa Sửa thành công user:{string.Join(" ", result.Errors.Select(e => e.Description))}");

                    }
                }
                return Ok("Sửa thành công");

            }
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) { return NotFound(); }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("Xóa thành công");
                }
                return BadRequest("Xóa thất bại");
            }

        }
    }
}
