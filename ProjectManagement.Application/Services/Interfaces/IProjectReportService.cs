using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface IProjectReportService
    {
        Task<ProjectReportViewModel> GetAll(int userId, int projectId);
    }
}
