using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectManagement.Application.Services.Implementations
{
    public class ProjectReportService : IProjectReportService
    {
        private readonly IJobLogRepository _jobLogRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectReportService(IJobLogRepository jobLogRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _jobLogRepository = jobLogRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectReportViewModel> GetAll(int userId, int projectId)
        {
            var model = new ProjectReportViewModel();

            model.ProjectCaption = (await _projectRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId))?.Caption ?? "";

            var details = await _jobLogRepository.All.AsNoTracking()
                .Where(l => l.Job.Split.ProjectId == projectId && l.Job.Split.Project.UserId == userId)
                .ToListAsync();

            model.Details = _mapper.Map<List<JobLogViewModel>>(details);

            return model;
        }
    }
}
