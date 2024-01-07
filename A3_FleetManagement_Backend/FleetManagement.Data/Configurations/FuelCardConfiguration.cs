using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Data.Configurations;

public class FuelCardConfiguration : IEntityTypeConfiguration<FuelCardValue>
{
    public void Configure(EntityTypeBuilder<FuelCardValue> builder)
    {
        // Configure the properties
        builder.HasKey(e => e.FuelCardID);
        builder.Property(e => e.FuelCardID).ValueGeneratedOnAdd();
        builder.Property(e => e.CardNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(e => e.CardNumber).IsUnique();
        builder.Property(e => e.ExpirationDate).HasColumnType("date");
        builder.Property(e => e.PinCode).HasMaxLength(10);
        builder.Property(e => e.AcceptedFuels).HasMaxLength(255);

        // Configure the table name
        builder.ToTable("FuelCard");
    }
}