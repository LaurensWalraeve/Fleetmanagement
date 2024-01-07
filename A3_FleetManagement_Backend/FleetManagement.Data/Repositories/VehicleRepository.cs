using AutoMapper;
using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;
using FleetManagement.Common.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using FleetManagement.Data.Repositories;
using FleetManagement.Common.Exceptions;

public class VehicleRepository : IVehicleRepository
{
    private readonly FleetDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDriverRepository _driverRepository;

    public VehicleRepository(FleetDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _driverRepository = new DriverRepository(context,mapper);
    }

    public async Task<List<Vehicle>> GetAllVehicles()
    {
        try
        {
            var vehiclesValue = await _context.Vehicles
                .Include(v => v.VehicleType)
                .Include(v => v.FuelType)
                .Where(v => v.DeletedAt == null)
                .ToListAsync();

            return _mapper.Map<List<Vehicle>>(vehiclesValue);
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to retrieve all vehicles.", ex);
        }
    }

    public async Task<List<Vehicle>> GetFilteredVehicles(string filter)
    {
        try
        {
            var vehiclesValue = await _context.Vehicles
                .Include(v => v.VehicleType)
                .Include(v => v.FuelType)
                .Where(vehicle => vehicle.LicensePlate.Contains(filter) && vehicle.DeletedAt == null)
                .ToListAsync();

            return _mapper.Map<List<Vehicle>>(vehiclesValue);
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to retrieve filtered vehicles.", ex);
        }
    }
    public async Task<Vehicle> GetVehicleById(int id)
    {
        try
        {
            var vehicleValue = await _context.Vehicles
                .Include(v => v.FuelType)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(v => v.VehicleID == id && v.DeletedAt == null);

            return _mapper.Map<Vehicle>(vehicleValue);
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to retrieve the vehicle by ID.", ex);
        }
    }

    public async Task<List<VehicleType>> GetAllVehicleTypes()
    {
        try
        {
            var vehicleTypesValue = await _context.VehicleTypes.ToListAsync();
            return _mapper.Map<List<VehicleType>>(vehicleTypesValue);
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to retrieve all vehicle types.", ex);
        }
    }

    public async Task CreateVehicle(Vehicle vehicle)
    {
        try
        {
            var currentVehicles = await GetAllVehicles();
            bool doesVehicleExist = currentVehicles.Any(v => v.ChassisNumber == vehicle.ChassisNumber);
            if (doesVehicleExist)
            {
                throw new ArgumentException(nameof(vehicle), "This vehicle already exists");
            }
            var vehicleValue = _mapper.Map<VehicleValue>(vehicle);
            _context.Vehicles.Add(vehicleValue);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to create the vehicle "+vehicle.ChassisNumber, ex);
        }
    }
    public async Task UpdateVehicle(Vehicle vehicle)
    {
        try
        {
            var vehicleValue = _mapper.Map<VehicleValue>(vehicle);
            _context.Entry(vehicleValue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new VehicleException("Failed to update the vehicle " + vehicle.ChassisNumber, ex);
        }
    }

    public async Task DeleteVehicle(int vehicleId)
    {
        try
        {
            var vehicleValue = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicleValue != null)
            {
                //_context.Vehicles.Remove(vehicleValue);
                vehicleValue.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var v = await GetVehicleById(vehicleId);
            throw new VehicleException("Failed to delete the vehicle "+ v.ChassisNumber, ex);
        }
    }

    public async Task<int?> GetVehicleRelation(int vehicleId)
    {
        try
        {
            var driverVehicle = await _context.DriverVehicles.FirstOrDefaultAsync(dv => dv.VehicleID == vehicleId);

            if (driverVehicle != null)
            {
                return driverVehicle.DriverID;
            }

            return null;
        }
        catch (Exception ex)
        {
            var v = await GetVehicleById(vehicleId);
            throw new VehicleException("Failed to get vehicle relation for "+v.ChassisNumber, ex);
        }
    }

}