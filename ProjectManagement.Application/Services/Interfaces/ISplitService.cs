using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface ISplitService
    {
        Task<List<SplitViewModel>> GetAll(int userId, int projectId = 0);

        Task<SplitViewModel> GetById(int id, int userId);

        Task<OperationResult> Create(SplitViewModel model);

        Task<OperationResult> Edit(SplitViewModel model);

        Task<OperationResult> Delete(int id, int userId);
    }
}
