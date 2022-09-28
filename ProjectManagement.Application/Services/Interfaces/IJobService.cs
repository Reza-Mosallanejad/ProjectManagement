using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface IJobService
    {
        Task<List<JobInfoViewModel>> GetAllInfo(int userId, int projectId = 0, int splitId = 0);

        Task<JobViewModel> GetById(int id, int userId);

        Task<OperationResult> Create(JobViewModel model);

        Task<OperationResult> Edit(JobViewModel model);

        Task<OperationResult> Delete(int id, int userId);
    }
}
