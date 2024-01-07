using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleTypeValue>
    {
        public void Configure(EntityTypeBuilder<VehicleTypeValue> builder)
        {
            builder.ToTable("VehicleType");

            builder.HasKey(vt => vt.VehicleTypeID);
            builder.Property(vt => vt.TypeName).IsRequired().HasMaxLength(255);

            // Define relationships (if any)
            // Here, you can configure relationships with other entities if needed

            // Additional configuration, if needed
        }
    }
}
