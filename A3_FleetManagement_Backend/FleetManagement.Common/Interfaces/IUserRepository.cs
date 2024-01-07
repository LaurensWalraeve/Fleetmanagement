using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUserName(string userName);
    Task<List<User>> GetFilteredUsers(string filter);
    Task<User> LogUserIn(string userName, string password);
    Task CreateUser(User user);
    Task<Role> GetUserRole(int roleID);
    Task<User> UpdateUser(int id, JObject driverJson);
    Task DeleteUser(User user);
    Task<List<User>> GetAllUsers();
}