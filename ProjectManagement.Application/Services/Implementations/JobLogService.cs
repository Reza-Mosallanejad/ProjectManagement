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
    public class JobLogService : IJobLogService
    {
        private readonly IJobLogRepository _jobLogRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobLogService(IJobLogRepository jobLogRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _jobLogRepository = jobLogRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<List<JobLogViewModel>> GetAll(int userId, int jobId)
        {
            var entities = await _jobLogRepository.All.AsNoTracking()
                .Where(l => l.Job.Split.Project.UserId == userId && l.JobId == jobId).ToListAsync();
            return _mapper.Map<List<JobLogViewModel>>(entities);
        }

        public async Task<bool> IsActive(int userId, int jobId)
        {
            return await _jobLogRepository.All.AsNoTracking()
                .AnyAsync(l => l.JobId == jobId && l.Job.Split.Project.UserId == userId && l.StopDate == null);
        }

        public async Task<OperationResult> Start(JobLogActivityViewModel model)
        {
            var opr = await ViewModelValidator(model);

            if (opr.Status)
            {
                var entity = new JobLog
                {
                    JobId = model.JobId,
                    StartDate = model.Date,
                    StartDescription = model.Description,
                };
                opr = await _jobLogRepository.AddAsync(entity);
                opr = await _jobLogRepository.SaveAsync(opr);
            }

            return opr;
        }

        public async Task<OperationResult> Stop(JobLogActivityViewModel model)
        {
            var opr = await ViewModelValidator(model);

            if (opr.Status)
            {
                var entity = await _jobLogRepository.All.AsNoTracking()
                    .Where(l => l.JobId == model.JobId && l.StopDate == null)
                    .OrderByDescending(l => l.Id)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    opr = OperationResult.Fail();

                if (opr.Status && entity.StartDate >= model.Date)
                    opr = OperationResult.Fail("Stop Date should be greater than start date time.");

                if (opr.Status && (int)(model.Date - entity.StartDate).TotalMinutes < 30)
                    opr = OperationResult.Fail("Duration can not be less than 30 minutes.");

                if (opr.Status)
                {
                    entity.StopDate = model.Date;
                    entity.StopDescription = model.Description;
                    entity.Duration = (int)(entity.StopDate.Value - entity.StartDate).TotalMinutes;

                    opr = _jobLogRepository.Update(entity);
                    opr = await _jobLogRepository.SaveAsync(opr);
                }
            }

            return opr;
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            if (!await _jobLogRepository.All.AsNoTracking().AnyAsync(l => l.Id == id && l.Job.Split.Project.UserId == userId))
                return OperationResult.Fail("Forbiden");

            var opr = await _jobLogRepository.Remove(id);
            opr = await _jobLogRepository.SaveAsync(opr);

            return opr;
        }

        private async Task<OperationResult> ViewModelValidator(JobLogActivityViewModel model)
        {
            var opr = OperationResult.Success();

            //user only can edit his projects
            if (opr.Status && !await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == model.JobId && j.Split.Project.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            //project is end
            if (!await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == model.JobId && j.Split.Project.ToDate >= DateTime.Now))
                opr = OperationResult.Fail("Project is expired and you can't edit that.");

            if (!await _jobRepository.All.AsNoTracking().AnyAsync(j => j.Id == model.JobId && j.Split.ToDate >= model.Date && j.Split.FromDate <= model.Date))
                opr = OperationResult.Fail("Selected date and time should be between start and end of split");

            return opr;
        }
    }
}
