using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations
{
    public class FuelCardAcceptedFuelsConfiguration : IEntityTypeConfiguration<FuelCardAcceptedFuelsValue>
    {
        public void Configure(EntityTypeBuilder<FuelCardAcceptedFuelsValue> builder)
        {
            builder.ToTable("FuelCardAcceptedFuels");

            // Define relationships
            builder.HasOne(ff => ff.FuelCard)
                .WithMany(f => f.AcceptedFuels)
                .HasForeignKey(ff => ff.FuelCardId);

            builder.HasOne(ff => ff.FuelType)  // Corrected relationship
                .WithMany() 
                .HasForeignKey(ff => ff.FuelTypeId);

            // Define the composite primary key using Fluent API
            builder.HasKey(ff => new { ff.FuelCardId, ff.FuelTypeId });

            // Additional configuration, if needed
        }
    }
}
