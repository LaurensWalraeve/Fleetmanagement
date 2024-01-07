using FleetManagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleValue>
    {
        public void Configure(EntityTypeBuilder<RoleValue> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(r => r.RoleID);
            builder.Property(r => r.RoleName).IsRequired().HasMaxLength(255);

        }
    }
}
