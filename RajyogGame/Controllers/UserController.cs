using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace RajyogGame.Controllers
{ 

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound("User not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Uid }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || id != user.Uid)
                return BadRequest("Invalid request.");

            await _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.RemoveUser(id);
            return NoContent();
        }
    }

}
