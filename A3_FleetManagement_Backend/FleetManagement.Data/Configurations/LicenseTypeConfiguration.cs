using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations
{
    public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseTypeValue>
    {
        public void Configure(EntityTypeBuilder<LicenseTypeValue> builder)
        {
            builder.ToTable("Licenses");

            builder.HasKey(lt => lt.LicenseTypeID);
            builder.Property(lt => lt.TypeName).IsRequired().HasMaxLength(255);
            builder.Property(lt => lt.Description).IsRequired().HasMaxLength(255);

            // Define relationships (if any)
            // Here, you can configure relationships with other entities if needed

            // Additional configuration, if needed
        }
    }
}
