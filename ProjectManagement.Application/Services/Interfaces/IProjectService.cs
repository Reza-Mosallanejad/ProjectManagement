using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectViewModel>> GetAll(int userId);

        Task<ProjectViewModel> GetById(int id, int userId);

        Task<OperationResult> Create(ProjectViewModel model);

        Task<OperationResult> Edit(ProjectViewModel model);

        Task<OperationResult> Delete(int id, int userId);
    }
}
