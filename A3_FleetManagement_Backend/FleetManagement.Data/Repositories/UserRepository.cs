using AutoMapper;
using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Repositories.Interfaces;
using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Security.Authentication;

namespace FleetManagement.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FleetDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(FleetDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<User>> GetAllUsers()
    {
        try
        {
            var users = await _context.Users
    .Include(u => u.Role)
    .Include(u => u.Driver)
        .ThenInclude(dv => dv.Licenses)
            .ThenInclude(dl => dl.LicenseType)
        .Include(u => u.Driver.Address)
        .Include(u => u.Driver.Vehicles)
            .ThenInclude(dv => dv.Vehicle)
                .ThenInclude(v => v.FuelType)
        .Include(u => u.Driver.Vehicles)
            .ThenInclude(dv => dv.Vehicle)
                .ThenInclude(v => v.VehicleType)
        .Include(u => u.Driver.FuelCards)
            .ThenInclude(df => df.FuelCard)
                .ThenInclude(fc => fc.AcceptedFuels)
                    .ThenInclude(fca => fca.FuelType)
    .Where(u => u.Driver.DeletedAt == null) // Voeg deze regel toe
    .ToListAsync();



            var mappedUsers = users.Select(userValue =>
            {
                var user = _mapper.Map<User>(userValue);
                user.Driver = _mapper.Map<Driver>(userValue.Driver);
                user.Driver.Address = _mapper.Map<Address>(userValue.Driver.Address);
                user.Driver.Vehicles = _mapper.Map<List<Vehicle>>(userValue.Driver.Vehicles.Select(v => v.Vehicle).ToList());
                user.Driver.FuelCards = _mapper.Map<List<FuelCard>>(userValue.Driver.FuelCards.Select(fc => fc.FuelCard).ToList());
                user.Driver.Licenses = userValue.Driver.Licenses
                    .Select(dl => _mapper.Map<DriverLicense>(dl))
                    .ToList();
                return user;
            }).ToList();
            return mappedUsers;
        }
        catch (Exception ex)
        {
            throw new UserException("Failed to get all users", ex);
        }
    }

    public async Task<List<User>> GetFilteredUsers(string filter)
    {
        try
        {
            var users = await _context.Users
    .Include(u => u.Role)
    .Include(u => u.Driver)
        .ThenInclude(dv => dv.Licenses)
            .ThenInclude(dl => dl.LicenseType)
        .Include(u => u.Driver.Address)
        .Include(u => u.Driver.Vehicles)
            .ThenInclude(dv => dv.Vehicle)
                .ThenInclude(v => v.FuelType)
        .Include(u => u.Driver.Vehicles)
            .ThenInclude(dv => dv.Vehicle)
                .ThenInclude(v => v.VehicleType)
        .Include(u => u.Driver.FuelCards)
            .ThenInclude(df => df.FuelCard)
                .ThenInclude(fc => fc.AcceptedFuels)
                    .ThenInclude(fca => fca.FuelType)
    .Where(user => user.Username.Contains(filter) && user.Driver.DeletedAt == null) // Voeg deze regel toe
    .ToListAsync();



            var mappedUsers = users.Select(userValue =>
            {
                var user = _mapper.Map<User>(userValue);
                user.Driver = _mapper.Map<Driver>(userValue.Driver);
                user.Driver.Address = _mapper.Map<Address>(userValue.Driver.Address);
                user.Driver.Vehicles = _mapper.Map<List<Vehicle>>(userValue.Driver.Vehicles.Select(v => v.Vehicle).ToList());
                user.Driver.FuelCards = _mapper.Map<List<FuelCard>>(userValue.Driver.FuelCards.Select(fc => fc.FuelCard).ToList());
                user.Driver.Licenses = userValue.Driver.Licenses
                    .Select(dl => _mapper.Map<DriverLicense>(dl))
                    .ToList();
                return user;
            }).ToList();
            return mappedUsers;
        }
        catch (Exception ex)
        {
            throw new UserException("Failed to get all filtered users", ex);
        }
    }

    public async Task<User> LogUserIn(string userName, string password)
    {
        try
        {
            var userValue = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == userName);

            if (userValue == null)
                return null;

            var user = _mapper.Map<User>(userValue);
            //await GetAllUsers();
            if (user.VerifyPassword(password))
            {
                var loggedInUserValue = await _context.Users
                    .Include(u => u.Driver)
                    .ThenInclude(dv => dv.Licenses)
                    .ThenInclude(dl => dl.LicenseType)
                    .Include(u => u.Driver.Address)
                    .Include(u => u.Driver.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                    .ThenInclude(v => v.FuelType)
                    .Include(u => u.Driver.Vehicles)
                    .ThenInclude(dv => dv.Vehicle)
                    .ThenInclude(v => v.VehicleType)
                    .Include(u => u.Driver.FuelCards)
                    .ThenInclude(df => df.FuelCard)
                    .ThenInclude(fc => fc.AcceptedFuels)
                    .ThenInclude(fca => fca.FuelType)
                    .FirstOrDefaultAsync();

                user.Driver = _mapper.Map<Driver>(loggedInUserValue.Driver);
                user.Driver.Address = _mapper.Map<Address>(loggedInUserValue.Driver.Address);
                user.Driver.Vehicles = _mapper.Map<List<Vehicle>>(loggedInUserValue.Driver.Vehicles.Select(v => v.Vehicle).ToList());
                user.Driver.FuelCards = _mapper.Map<List<FuelCard>>(loggedInUserValue.Driver.FuelCards.Select(fc => fc.FuelCard).ToList());
                user.Driver.Licenses = loggedInUserValue.Driver.Licenses
                    .Select(dl => _mapper.Map<DriverLicense>(dl))
                    .ToList();

                return user;
            }
            else
            {
                throw new AuthenticationException("Password does not match");
            }
        }
        catch (Exception ex)
        {
            throw new AuthenticationException("Authentication failed. Check username", ex);
        }
    }

    public async Task<User> UpdateUser(int userId, JObject userJson)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Zoek de gebruiker in de database
                var userFromDb = await _context.Users
    .Include(u => u.Driver)
    .Include(u => u.Role)
    .FirstOrDefaultAsync(u => u.UserID == userId);


                if (userFromDb == null)
                {
                    throw new NotFoundException("User not found.");
                }

                // Haal de waarden uit de JSON op en pas deze aan op het object
                var username = userJson.GetValue("username")?.ToString();
                var password = userJson.GetValue("password")?.ToString();
                var roleId = userJson.GetValue("roleId")?.ToObject<int>();
                var driverId = userJson.GetValue("driverId")?.ToObject<int>();

                if (username != null && username != "exampleUsername")
                {
                    userFromDb.Username = username;
                }

                if (password != null && password != "examplePassword")
                {
                    var user = _mapper.Map<User>(userFromDb);
                    user.Password = password;
                    userFromDb.Password = user.Password;
                    
                }

                if (roleId.HasValue && roleId != 0)
                {
                    userFromDb.Role = await _context.Roles.FindAsync(roleId);
                }

                if (driverId.HasValue && driverId != 0)
                {
                    userFromDb.DriverID = driverId;
                }

                // Bewaar de wijzigingen in de database
                _context.Users.Update(userFromDb);
                await _context.SaveChangesAsync();

                // Commit de transactie
                await transaction.CommitAsync();

                // Vervang ConvertToDomainModel door de juiste AutoMapper-mapping
                var updatedUser = _mapper.Map<User>(userFromDb);
                return updatedUser;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public async Task DeleteUser(User user)
    {
        try
        {
            var existingUserValue = await _context.Users.FindAsync(user.UserID);

            if (existingUserValue != null)
            {
                //_context.Users.Remove(existingUserValue);
                existingUserValue.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                await GetAllUsers();
            }
        }
        catch (Exception ex)
        {
            throw new UserException("Error while deleting user, check if username is correct " + user.Username, ex);
        }

    }

    public async Task<User> GetUserByUserName(string userName)
    {
        try
        {
            List<User> users = new List<User>();
            users = await GetAllUsers();
            User result = null;
            foreach (var user in users)
            {
                if (user.Username == userName)
                {
                    result = user;
                    break;
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new UserException("Error while getting user by username " + userName, ex);
        }
    }
    public async Task CreateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentException("User data must be provided.");
        }

        var currentUsers = await GetAllUsers();
        bool doesUserExist = currentUsers.Any(u => u.Username == user.Username);
        if (doesUserExist)
        {
            throw new ArgumentException(nameof(user), "This user already exists");
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Create a new driver entity
                var userEntity = _mapper.Map<UserValue>(user);
                userEntity.DriverID = user.Driver.DriverId;

                var existingRole = _context.Roles.Find(user.Role.RoleID);
                if (existingRole != null)
                {
                    userEntity.Role = existingRole;
                }
                else
                {
                    throw new Exception("Foute RoleID opgegeven");
                }
                var existingDriver = _context.Drivers.Find(user.Driver.DriverId);
                if (existingDriver != null)
                {
                    userEntity.Driver = existingDriver;
                }
                else
                {
                    throw new Exception("Foute DriverID opgegeven");
                }

                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();
                // Retrieve the complete user object from the database
                var createdUser = await _context.Users.FindAsync(userEntity.UserID);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new UserException("Error while creating user "+user.Username, ex);
            }
        }

    }

    public async Task<Role> GetUserRole(int roleID)
    {
        try
        {
            if (roleID == 0)
                throw new ArgumentNullException("RoleID must be provided");

            var roleValue = await _context.Roles
                .Where(role => role.RoleID == roleID)
                .FirstOrDefaultAsync();

            if (roleValue != null)
            {
                return _mapper.Map<Role>(roleValue);
            }
           
            return null;
        }
        catch (Exception ex)
        {
            throw new UserException("Error getting user role", ex);
        }
    }
}
