using FleetManagement.Common.Models;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Services.Interfaces;

public interface IDriverService
{
    Task<List<Driver>> GetAllDrivers();
    Task<List<Driver>> GetFilteredDrivers(string filter);
    Task<Driver> GetDriverById(int id);
    Task<List<LicenseType>> GetAllLicenseTypes();
    Task<Driver> CreateDriver(Driver driver);
    Task<Driver> UpdateDriver(int id, JObject driverJson);
    Task DeleteDriver(Driver driver);
}