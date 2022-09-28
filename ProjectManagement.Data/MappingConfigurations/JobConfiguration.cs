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
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Caption)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.HasOne(j => j.Split)
                .WithMany(s => s.Jobs)
                .HasForeignKey(j => j.SplitId);
        }
    }
}
