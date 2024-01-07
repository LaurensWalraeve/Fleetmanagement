using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<UserValue>
{
    public void Configure(EntityTypeBuilder<UserValue> builder)
    {
        // Configure the properties
        builder.HasKey(e => e.UserID);
        builder.Property(e => e.UserID).ValueGeneratedOnAdd();
        builder.Property(e => e.Username).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Password).IsRequired().HasMaxLength(255);

        builder.HasIndex(e => e.Username).IsUnique();

        builder.HasOne(e => e.Driver) // Assuming there's a navigation property named "Driver" in the User entity.
            .WithMany()
            .HasForeignKey(e => e.DriverID) // Assuming "DriverId" is a foreign key property in the User entity.
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Role) // Assuming there's a navigation property named "Role" in the User entity.
            .WithMany()
            .HasForeignKey(e => e.Role) // Assuming "RoleId" is a foreign key property in the User entity.
            .OnDelete(DeleteBehavior.Restrict); // Or .OnDelete(DeleteBehavior.Cascade) as needed

        // Configure the table name
        builder.ToTable("User");

        // You can also set character set and collation if needed
        builder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");
    }
}
