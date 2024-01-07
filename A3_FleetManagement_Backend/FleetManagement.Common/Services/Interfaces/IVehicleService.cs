using FleetManagement.Common.Models;

namespace FleetManagement.Common.Services.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAllVehicles();
    Task<int?> GetVehicleRelation(int id);
    Task<List<Vehicle>> GetFilteredVehicles(string filter);
    Task<Vehicle> GetVehicleById(int id);
    Task<List<VehicleType>> GetAllVehicleTypes();
    Task CreateVehicle(Vehicle vehicle);
    Task UpdateVehicle(Vehicle vehicle);
    Task DeleteVehicle(int id);
}