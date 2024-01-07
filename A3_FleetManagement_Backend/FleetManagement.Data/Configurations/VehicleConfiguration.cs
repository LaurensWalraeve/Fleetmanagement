using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<VehicleValue>
{
    public void Configure(EntityTypeBuilder<VehicleValue> builder)
    {
        // Configure the properties
        builder.HasKey(e => e.VehicleID);
        builder.Property(e => e.VehicleID).ValueGeneratedOnAdd();
        builder.Property(e => e.Make).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Model).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ChassisNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(e => e.ChassisNumber).IsUnique();
        builder.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
        builder.HasIndex(e => e.LicensePlate).IsUnique();

        // Vreemde sleutel naar FuelTypeValue
        builder.Property(e => e.FuelTypeID);
        builder.HasOne(e => e.FuelType)
            .WithMany()
            .HasForeignKey(e => e.FuelTypeID);

        // Vreemde sleutel naar VehicleTypeValue
        builder.Property(e => e.VehicleTypeID);
        builder.HasOne(e => e.VehicleType)
            .WithMany()
            .HasForeignKey(e => e.VehicleTypeID);

        builder.Property(e => e.Color).HasMaxLength(50);
        builder.Property(e => e.NumberOfDoors);

        // Configure the table name
        builder.ToTable("Vehicle");

        // You can also set character set and collation if needed
        builder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");
    }
}
