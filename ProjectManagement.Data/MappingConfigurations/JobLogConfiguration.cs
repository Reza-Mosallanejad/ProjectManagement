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
    public class JobLogConfiguration : IEntityTypeConfiguration<JobLog>
    {
        public void Configure(EntityTypeBuilder<JobLog> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.Job)
                .WithMany(j => j.JobLogs)
                .HasForeignKey(l => l.JobId);
        }
    }
}
