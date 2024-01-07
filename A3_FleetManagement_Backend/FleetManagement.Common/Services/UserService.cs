using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task<List<User>> GetFilteredUsers(string filter)
        {
            try
            {
                return await _userRepository.GetFilteredUsers(filter);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task<Role> GetUserRole(int roleID)
        {
            try
            {
                return await _userRepository.GetUserRole(roleID);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            try
            {
                return await _userRepository.GetUserByUserName(userName);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task<User> LogUserIn(string userName, string password)
        {
            try
            {
                return await _userRepository.LogUserIn(userName, password);
            }
            catch (Exception ex)
            {
                throw new UserLogInException(ex.Message, ex);
            }
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task<User> UpdateUser(int id, JObject userJson)
        {
            try
            {
                return await _userRepository.UpdateUser(id, userJson);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public async Task DeleteUser(User user)
        {
            try
            {
                await _userRepository.DeleteUser(user);
            }
            catch (Exception ex)
            {
                throw new UserException(ex.Message, ex);
            }
        }

        public Task<User> LogUserOut(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
