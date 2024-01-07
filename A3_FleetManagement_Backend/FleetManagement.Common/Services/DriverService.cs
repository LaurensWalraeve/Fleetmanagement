using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Common.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace FleetManagement.Common.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            try
            {
                return await _driverRepository.GetAllDrivers();
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task<List<Driver>> GetFilteredDrivers(string filter)
        {
            try
            {
                return await _driverRepository.GetFilteredDrivers(filter);
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task<Driver> GetDriverById(int id)
        {
            try
            {
                return await _driverRepository.GetDriverById(id);
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task<List<LicenseType>> GetAllLicenseTypes()
        {
            try
            {
                return await _driverRepository.GetAllLicenseTypes();
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task<Driver> CreateDriver(Driver driver)
        {
            try
            {
                return await _driverRepository.CreateDriver(driver);
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task<Driver> UpdateDriver(int id, JObject driverJson)
        {
            try
            {
                var driverToUpdate = driverJson.ToObject<DriverUpdateModel>();
                if (driverToUpdate == null)
                {
                    throw new ValidationException("Invalid input data.");
                }

                return await _driverRepository.UpdateDriver(id, driverToUpdate, driverJson);
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }

        public async Task DeleteDriver(Driver driver)
        {
            try
            {
                await _driverRepository.DeleteDriver(driver);
            }
            catch (Exception ex)
            {
                throw new DriverException(ex.Message, ex);
            }
        }
    }
}
