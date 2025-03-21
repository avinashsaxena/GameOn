using Data;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace RajyogGame.Controllers
{ 

    [ApiController]
    [Route("api/users")]
    public class UserController(IUserRepository userRepository, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation($"Fetching user with ID: {id}");

            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _logger.LogInformation("Adding a new user");
            await _userRepository.AddUser(user);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            _logger.LogInformation($"Updating user with ID: {user.Uid}");
            await _userRepository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");
            await _userRepository.DeleteUser(id);
            return Ok();
        }
    }
}
