using FleetManagement.Common.Models;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetFilteredUsers(string filter);
    Task<User> LogUserIn(string userName, string password);
    Task<User> LogUserOut(string userName, string password);
    Task<User> GetUserByUserName(string userName);
    Task<Role> GetUserRole(int roleID);
    Task CreateUser(User user);
    Task<User> UpdateUser(int id, JObject userJson);
    Task DeleteUser(User user);
}