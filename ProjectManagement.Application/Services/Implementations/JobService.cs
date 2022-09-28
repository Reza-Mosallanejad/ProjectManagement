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
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISplitRepository _splitRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, ISplitRepository splitRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _splitRepository = splitRepository;
            _mapper = mapper;
        }

        public async Task<List<JobInfoViewModel>> GetAllInfo(int userId, int projectId = 0, int splitId = 0)
        {
            var models = await _jobRepository.All.AsNoTracking()
                .Where(j =>
                    j.Split.Project.UserId == userId &&
                    (splitId > 0 ? j.SplitId == splitId : true) &&
                    (projectId > 0 ? j.Split.ProjectId == projectId : true)
                )
                .Select(j => new JobInfoViewModel
                {
                    Id = j.Id,
                    Caption = j.Caption,
                    IsActive = j.JobLogs.Any(l => !l.IsDelete && l.StopDate == null),
                    Duration = j.Duration,
                    SpentTime = j.JobLogs.Where(l => !l.IsDelete && l.StopDate.HasValue && l.Duration > 0).Sum(l => l.Duration),
                    ProjectCaption = j.Split.Project.Caption,
                    SplitCaption = j.Split.Caption,
                    SplitIsActive = j.Split.ToDate >= DateTime.Now,
                }).ToListAsync();

            return models;
        }

        public async Task<JobViewModel> GetById(int id, int userId)
        {
            var entity = await _jobRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id && j.Split.Project.UserId == userId);
            return _mapper.Map<JobViewModel>(entity);
        }

        public async Task<OperationResult> Create(JobViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Job>(model);
                op = await _jobRepository.AddAsync(entity);
                op = await _jobRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Edit(JobViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Job>(model);
                op = _jobRepository.Update(entity);
                op = await _jobRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            if (!await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == id && j.Split.Project.UserId == userId))
                return OperationResult.Fail("Forbiden");

            if (await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == id && j.JobLogs.Any()))
                return OperationResult.Fail("Please first delete Job Logs.");

            var op = await _jobRepository.Remove(id);
            op = await _jobRepository.SaveAsync(op);

            return op;
        }

        private async Task<OperationResult> ViewModelValidator(JobViewModel model)
        {
            var opr = OperationResult.Success();

            //user only can edit his projects
            if (opr.Status && model.Id > 0 && !await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == model.Id && j.Split.Project.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            //job can be asign only to splits that user has access on its project
            if (opr.Status && !await _splitRepository.All.AsNoTracking().AnyAsync(s => s.Id == model.SplitId && s.Project.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            //project is expired
            if (opr.Status && !await _splitRepository.All.AsNoTracking().AnyAsync(s => s.Id == model.SplitId && s.ToDate <= DateTime.Now))
                opr = OperationResult.Fail("Split is expired and you can edit this informations.");


            return opr;
        }
    }
}
