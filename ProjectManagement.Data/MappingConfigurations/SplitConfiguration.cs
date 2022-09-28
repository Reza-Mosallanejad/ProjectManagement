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
    public class SplitConfiguration : IEntityTypeConfiguration<Split>
    {
        public void Configure(EntityTypeBuilder<Split> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Caption)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.HasOne(s => s.Project)
                .WithMany(p => p.Splits)
                .HasForeignKey(s => s.ProjectId);
        }
    }
}
