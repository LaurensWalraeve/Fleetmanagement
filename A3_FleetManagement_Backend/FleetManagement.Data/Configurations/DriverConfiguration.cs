using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DriverConfiguration : IEntityTypeConfiguration<DriverValue>
{
    public void Configure(EntityTypeBuilder<DriverValue> builder)
    {
        // Configure the properties
        builder.HasKey(e => e.DriverID);
        builder.Property(e => e.DriverID).ValueGeneratedOnAdd();
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Birthdate).HasColumnType("date");
        builder.Property(e => e.SocialSecurityNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(e => e.SocialSecurityNumber).IsUnique();
        builder.Property(e => e.AddressID);

        builder.HasOne(e => e.Address)
            .WithMany()
            .HasForeignKey(e => e.AddressID)
            .OnDelete(DeleteBehavior.Restrict); // Of .OnDelete(DeleteBehavior.Cascade) zoals nodig

        // Configure the relationship with Licenses
        builder.HasMany(e => e.Licenses) // Dit veronderstelt dat er een navigatie-eigenschap 'Licenses' in de 'DriverValue'-entiteit is
            .WithOne(license => license.Driver) // Dit is de navigatie-eigenschap in de 'DriverLicenseValue'-entiteit
            .HasForeignKey(license => license.DriverID);



        // Configure the table name
        builder.ToTable("Driver");

        // You can also set character set and collation if needed
        builder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");
    }
}