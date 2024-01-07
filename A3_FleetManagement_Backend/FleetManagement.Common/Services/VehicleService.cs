using FleetManagement.Common.Exceptions;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using FleetManagement.Common.Repositories.Interfaces;

namespace FleetManagement.Common.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            try
            {
                return await _vehicleRepository.GetAllVehicles();
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task<List<Vehicle>> GetFilteredVehicles(string filter)
        {
            try
            {
                return await _vehicleRepository.GetFilteredVehicles(filter);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            try
            {
                return await _vehicleRepository.GetVehicleById(id);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task<List<VehicleType>> GetAllVehicleTypes()
        {
            try
            {
                return await _vehicleRepository.GetAllVehicleTypes();
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            try
            {
                await _vehicleRepository.CreateVehicle(vehicle);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                await _vehicleRepository.UpdateVehicle(vehicle);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task DeleteVehicle(int id)
        {
            try
            {
                await _vehicleRepository.DeleteVehicle(id);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }

        public async Task<int?> GetVehicleRelation(int id)
        {
            try
            {
                return await _vehicleRepository.GetVehicleRelation(id);
            }
            catch (Exception ex)
            {
                throw new VehicleException(ex.Message, ex);
            }
        }
    }
}
