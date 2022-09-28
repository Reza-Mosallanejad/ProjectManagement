using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface IJobLogService
    {
        Task<List<JobLogViewModel>> GetAll(int userId, int jobId);

        Task<bool> IsActive(int userId, int jobId);

        Task<OperationResult> Start(JobLogActivityViewModel model);

        Task<OperationResult> Stop(JobLogActivityViewModel model);

        Task<OperationResult> Delete(int id, int userId);
    }
}
