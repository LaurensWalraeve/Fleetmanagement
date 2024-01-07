using FleetManagement.Common.Models;
using FleetManagement.Data.Configurations;
using FleetManagement.Data.Contexts;
using FleetManagement.Data.Contexts.Interfaces;
using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class FleetDbContext : BaseDbContext, IFleetDbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options)
        : base(options)
    {
    }

    public DbSet<DriverValue> Drivers { get; set; } = null!;
    public DbSet<FuelCardValue> FuelCards { get; set; } = null!;
    public DbSet<VehicleValue> Vehicles { get; set; } = null!;
    public DbSet<AddressValue> Addresses { get; set; } = null!;
    public DbSet<DriverFuelCardValue> DriverFuelCards { get; set; } = null!;
    public DbSet<DriverVehicleValue> DriverVehicles { get; set; } = null!;
    public DbSet<VehicleTypeValue> VehicleTypes { get; set; } = null!;
    public DbSet<UserValue> Users { get; set; } = null!;
    public DbSet<FuelTypeValue> FuelTypes { get; set; } = null!;
    public DbSet<FuelCardAcceptedFuelsValue> FuelCardAcceptedFuels { get; set; } = null!;
    public DbSet<DriverLicenseValue> DriverLicenses { get; set; } = null!;
    public DbSet<RoleValue> Roles { get; set; } = null!;
    public DbSet<LicenseTypeValue> LicenseTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DriverConfiguration());

        // Geef de tabelnaam expliciet aan voor de DriverLicenseValue-entiteit
        modelBuilder.Entity<DriverLicenseValue>().ToTable("DriverLicense");
        modelBuilder.Entity<DriverVehicleValue>().ToTable("DriverVehicle");
        modelBuilder.Entity<FuelCardAcceptedFuelsValue>().ToTable("FuelCardAcceptedFuels");
        modelBuilder.Entity<DriverFuelCardValue>().ToTable("DriverFuelCard");
        modelBuilder.Entity<DriverFuelCardValue>().HasKey(dfc => new { dfc.DriverID, dfc.FuelCardID });
        modelBuilder.Entity<DriverLicenseValue>().HasKey(dlv => new { dlv.LicenseTypeID, dlv.DriverID });
        modelBuilder.Entity<DriverVehicleValue>().HasKey(dvv => new { dvv.DriverID, dvv.VehicleID });
        modelBuilder.Entity<FuelCardAcceptedFuelsValue>().HasKey(fca => new { fca.FuelCardId, fca.FuelTypeId });

    }

}