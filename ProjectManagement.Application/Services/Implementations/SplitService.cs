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
    public class SplitService : ISplitService
    {
        private readonly ISplitRepository _splitRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public SplitService(ISplitRepository splitRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _splitRepository = splitRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<SplitViewModel>> GetAll(int userId, int projectId = 0)
        {
            var entities = await _splitRepository.All.AsNoTracking()
                .Where(s => s.Project.UserId == userId && (projectId > 0 ? s.ProjectId == projectId : true))
                .ToListAsync();
            return _mapper.Map<List<SplitViewModel>>(entities);
        }

        public async Task<SplitViewModel> GetById(int id, int userId)
        {
            var entity = await _splitRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.Project.UserId == userId);
            return _mapper.Map<SplitViewModel>(entity);
        }

        public async Task<OperationResult> Create(SplitViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Split>(model);
                op = await _splitRepository.AddAsync(entity);
                op = await _splitRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Edit(SplitViewModel model)
        {
            var op = await ViewModelValidator(model);

            if (op.Status)
            {
                var entity = _mapper.Map<Split>(model);
                op = _splitRepository.Update(entity);
                op = await _splitRepository.SaveAsync(op);
            }

            return op;
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            if (!await _splitRepository.All.AsNoTracking().AnyAsync(s => s.Id == id && s.Project.UserId == userId))
                return OperationResult.Fail("Forbiden");

            if (await _splitRepository.All.AsNoTracking().AnyAsync(p => p.Id == id && p.Jobs.Any()))
                return OperationResult.Fail("Please first delete jobs.");

            var op = await _splitRepository.Remove(id);
            op = await _splitRepository.SaveAsync(op);

            return op;
        }

        private async Task<OperationResult> ViewModelValidator(SplitViewModel model)
        {
            var opr = OperationResult.Success();

            if (opr.Status && model.Id > 0 && !await _splitRepository.All.AsNoTracking().AnyAsync(s => s.Id == model.Id && s.Project.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            if (opr.Status && !await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == model.ProjectId && p.UserId == model.UserId))
                opr = OperationResult.Fail("Forbiden");

            //to date greater than from date
            if (opr.Status && model.FromDate >= model.ToDate)
                opr.Failed("ToDate should be greater than FromDate.");

            //datetime between project
            if (opr.Status && !await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == model.ProjectId && p.FromDate <= model.FromDate && p.ToDate >= model.ToDate))
                opr = OperationResult.Fail("Fom and To dates should be between project dates.");

            //project is expired
            if (opr.Status && !await _projectRepository.All.AsNoTracking().AnyAsync(p => p.Id == model.ProjectId && p.ToDate <= DateTime.Now))
                opr = OperationResult.Fail("Split project is expired and you can edit this informations.");

            return opr;
        }
    }
}
