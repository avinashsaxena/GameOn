using Data;
using Microsoft.AspNetCore.Mvc;

namespace RajyogGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccessController(IUserAccessRepository userAccessRepository, ILogger<UserAccessController> logger) : ControllerBase
    {
        private readonly IUserAccessRepository _userAccessRepository = userAccessRepository;
        private readonly ILogger<UserAccessController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetUserAccesses()
        {
            _logger.LogInformation("Fetching all user access records");
            return Ok(await _userAccessRepository.GetAllUserAccess());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAccess(int id)
        {
            _logger.LogInformation($"Fetching user access with ID: {id}");
            return Ok(await _userAccessRepository.GetUserAccessById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAccess([FromBody] UserAccess userAccess)
        {
            _logger.LogInformation("Adding a new user access record");
            await _userAccessRepository.AddUserAccess(userAccess);
            return Ok(userAccess);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAccess([FromBody] UserAccess userAccess)
        {
            _logger.LogInformation($"Updating user access with ID: {userAccess.Uaid}");
            await _userAccessRepository.UpdateUserAccess(userAccess);
            return Ok(userAccess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccess(int id)
        {
            _logger.LogInformation($"Deleting user access with ID: {id}");
            await _userAccessRepository.DeleteUserAccess(id);
            return Ok();
        }
    }
}