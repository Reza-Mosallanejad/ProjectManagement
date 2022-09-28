using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.MappingConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Username)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Password)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.Email)
               .IsRequired()
               .HasColumnType("varchar(100)");
        }
    }
}
