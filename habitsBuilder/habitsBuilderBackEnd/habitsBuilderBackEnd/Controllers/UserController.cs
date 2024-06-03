using habitsBuilderBackEnd.Models;
using habitsBuilderBackEnd.Repositories;
using habitsBuilderBackEnd.Services;
using habitsBuilderBackEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;

namespace habitsBuilderBackEnd.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            var createdUser = await userService.RegisterUserAsync(user);
            var userDTO = await userService.GetUserDTOAsync(createdUser.UserId);
            return CreatedAtAction(nameof(GetUser), new { id = userDTO.UserId }, userDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var userDTO = await userService.GetUserDTOAsync(id);
            if (userDTO == null)
            {
                return Ok(new { message = "用户不存在" });
            }
            return userDTO;
        }

        [HttpPost("{id}/update-password")]
        public async Task<ActionResult> UpdatePassword(string id, [FromBody] UpdatePasswordRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.NewPassword))
            {
                return Ok(new { message = "密码修改错误" });
            }

            var user = await userService.UpdatePasswordAsync(id, request.NewPassword);
            if (user == null)
            {
                return Ok(new { message = "用户不存在" });
            }
            return Ok(new { message = "修改成功" });
        }

        [HttpPost("{id}/friends/{friendId}")]
        public async Task<ActionResult> AddFriend(string id, string friendId)
        {
            var result = await userService.AddFriendAsync(id, friendId);
            if (!result)
            {
                return Ok(new { message = "用户不存在" });
            }
            return Ok(new { message = "好友已添加" });
        }

        [HttpDelete("{id}/friends/{friendId}")]
        public async Task<ActionResult> RemoveFriend(string id, string friendId)
        {
            var result = await userService.RemoveFriendAsync(id, friendId);
            if (!result)
            {
                return Ok(new { message = "用户不存在" });
            }
            return Ok(new { message = "好友已删除" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await userService.ValidateUserAsync(request.UserId, request.Password);
            if (user == null)
            {
                return Ok(new { success = false, message = "用户名或密码错误" });
            }

            // 登录成功后
            return Ok(new { success = true, message = "登录成功" });
        }
    }
    public class LoginRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
