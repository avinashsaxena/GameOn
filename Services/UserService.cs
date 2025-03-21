using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task RemoveUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetUsers() => await _userRepository.GetAllUsers();
        public async Task<User> GetUser(int id) => await _userRepository.GetUserById(id);
        public async Task CreateUser(User user)
        {
            _logger.LogInformation("Creating a new user");
            await _userRepository.AddUser(user);
        }
        public async Task UpdateUser(User user)
        {
            _logger.LogInformation("Updating user {UserId}", user.Uid);
            await _userRepository.UpdateUser(user);
        }
        public async Task RemoveUser(int id)
        {
            _logger.LogInformation("Deleting user {UserId}", id);
            await _userRepository.DeleteUser(id);
        }
    }
}
