using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<AddressValue>
{
    public void Configure(EntityTypeBuilder<AddressValue> builder)
    {
        // Configure the properties
        builder.HasKey(e => e.AddressID);
        builder.Property(e => e.City).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Street).IsRequired().HasMaxLength(50);
        builder.Property(e => e.HouseNumber).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ZipCode).IsRequired().HasMaxLength(50);

        // Configure the table name
        builder.ToTable("Address");

        // You can also set character set and collation if needed
        builder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");
    }
}