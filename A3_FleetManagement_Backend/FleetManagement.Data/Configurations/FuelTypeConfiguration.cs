using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations
{
    public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelTypeValue>
    {
        public void Configure(EntityTypeBuilder<FuelTypeValue> builder)
        {
            builder.ToTable("FuelType");

            builder.HasKey(ft => ft.FuelTypeID);
            builder.Property(ft => ft.TypeName).IsRequired().HasMaxLength(255);

            // Additional configuration, if needed
        }
    }
}
