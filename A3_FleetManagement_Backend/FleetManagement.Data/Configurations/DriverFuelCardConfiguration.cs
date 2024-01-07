using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations
{
    public class DriverFuelCardConfiguration : IEntityTypeConfiguration<DriverFuelCardValue>
    {
        public void Configure(EntityTypeBuilder<DriverFuelCardValue> builder)
        {
            builder.ToTable("DriverFuelCard");

            builder.HasKey(df => new { df.DriverID, df.FuelCardID });

            builder.Property(df => df.DriverID).IsRequired();
            builder.Property(df => df.FuelCardID).IsRequired();
            builder.Property(df => df.StartDate).HasColumnType("datetime");

            builder.HasOne(df => df.Driver)
                .WithMany(d => d.FuelCards)
                .HasForeignKey(df => df.DriverID);

            builder.HasOne(df => df.FuelCard)
                .WithMany(fc => fc.DriverFuelCards)
                .HasForeignKey(df => df.FuelCardID);
        }
    }
}
