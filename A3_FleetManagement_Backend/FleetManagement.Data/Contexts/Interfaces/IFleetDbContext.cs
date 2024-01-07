using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Data.Contexts.Interfaces;

public interface IFleetDbContext : IBaseDbContext
{
    DbSet<DriverValue> Drivers { get; set; }
    DbSet<FuelCardValue> FuelCards { get; set; }
    DbSet<VehicleValue> Vehicles { get; set; }
    DbSet<AddressValue> Addresses { get; set; }
    DbSet<DriverFuelCardValue> DriverFuelCards { get; set; }
    DbSet<DriverVehicleValue> DriverVehicles { get; set;}
    DbSet<VehicleTypeValue> VehicleTypes { get; set; }
    DbSet<UserValue> Users { get; set; }
    DbSet<FuelTypeValue> FuelTypes { get; set; }
    DbSet<FuelCardAcceptedFuelsValue> FuelCardAcceptedFuels { get; set; }
    DbSet<DriverLicenseValue> DriverLicenses { get; set; }
    DbSet<RoleValue> Roles { get; set; }
}