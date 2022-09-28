using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Services.Implementations;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Data.Context;
using ProjectManagement.Data.Repositories;
using ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.IoC
{
    public class DependencyContainer
    {
        public static void RegisterDependency(IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISplitRepository, SplitRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobLogRepository, JobLogRepository>();
            #endregion

            #region Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISplitService, SplitService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobLogService, JobLogService>();
            services.AddScoped<IProjectReportService, ProjectReportService>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

        }
    }
}
