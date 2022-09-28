using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    [Authorize]
    public class ProjectReportController : Controller
    {
        #region Ctor
        private readonly IProjectReportService _projectReportService;
        private readonly ILogger<ProjectReportController> _logger;

        public ProjectReportController(IProjectReportService projectReportService, ILogger<ProjectReportController> logger)
        {
            _projectReportService = projectReportService;
            _logger = logger;
        }
        #endregion

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var model = await _projectReportService.GetAll(User.GetUserId(), id);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

    }
}
