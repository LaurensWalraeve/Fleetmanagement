using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DriverLicenseConfiguration : IEntityTypeConfiguration<DriverLicenseValue>
{
    public void Configure(EntityTypeBuilder<DriverLicenseValue> builder)
    {
        builder.ToTable("DriverLicense");

        // Define relationships
        builder.HasOne(dl => dl.Driver)
            .WithMany(d => d.Licenses)
            .HasForeignKey(dl => dl.DriverID);

        builder.HasOne(dl => dl.LicenseType)
            .WithMany(lt => lt.DriverLicenses)
            .HasForeignKey(dl => dl.LicenseTypeID);

        // Define the composite primary key using Fluent API
        builder.HasKey(dl => new { dl.DriverID, dl.LicenseTypeID });

        // Additional configuration, if needed
    }
}
