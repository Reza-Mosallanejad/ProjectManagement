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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectViewModel>> GetAll(int userId)
        {
            var entities = await _projectRepository.All.AsNoTracking()
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<ProjectViewModel>>(entities);
        }

        public async Task<ProjectViewModel> GetById(int id, int userId)
        {
            var entity = await _projectRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            return _mapper.Map<ProjectViewModel>(entity);
        }

        public async Task<OperationResult> Create(ProjectViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Project>(model);
                op = await _projectRepository.AddAsync(entity);
                op = await _projectRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Edit(ProjectViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Project>(model);
                op = _projectRepository.Update(entity);
                op = await _projectRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            if (!await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == id && p.UserId == userId))
                return OperationResult.Fail("Forbiden");

            if (await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == id && p.Splits.Any()))
                return OperationResult.Fail("Please first delete project splits.");

            var op = await _projectRepository.Remove(id);
            op = await _projectRepository.SaveAsync(op);

            return op;
        }

        private async Task<OperationResult> ViewModelValidator(ProjectViewModel model)
        {
            var opr = OperationResult.Success();

            if (opr.Status && model.Id > 0 && !await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == model.Id && p.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            //to date greater than from date
            if (opr.Status && model.FromDate >= model.ToDate)
                opr.Failed("ToDate should be greater than FromDate.");

            return opr;
        }
    }
}
