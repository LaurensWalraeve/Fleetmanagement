using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Repositories.Interfaces;

public interface IDriverRepository
{
    Task<List<Driver>> GetAllDrivers();
    Task<List<Driver>> GetFilteredDrivers(string filter);
    Task<Driver> GetDriverById(int id);
    Task<List<LicenseType>> GetAllLicenseTypes();
    Task<Driver> CreateDriver(Driver driver);
    Task<Driver> UpdateDriver(int id, DriverUpdateModel updateModel, JObject driverJson);
    Task DeleteDriver(Driver driver);
}