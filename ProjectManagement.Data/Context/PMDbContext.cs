using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.MappingConfigurations;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Context
{
    public class PMDbContext : DbContext
    {
        public PMDbContext(DbContextOptions options) : base(options)
        {
        }

        public PMDbContext()
        {
        }

        #region DbSets
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new SplitConfiguration());
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new JobLogConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Split> Splits { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobLog> JobLogs { get; set; }
        #endregion
    }
}
