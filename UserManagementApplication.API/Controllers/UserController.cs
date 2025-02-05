using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementApplication.API.Dto;
using UserManagementApplication.Services.Data.Interfaces;

namespace UserManagementApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("GetSpecificUser/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("SearchUser")]
        public async Task<IActionResult> SearchAsync(string searchTerm)
        {
            var users = await _userService.SearchAsync(searchTerm);
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto userDto)
        {
            var result = await _userService.CreateAsync(userDto);
            return Ok(result);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateUserDto userDto)
        {
            var result = await _userService.UpdateAsync(id, userDto);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
