using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        #region Ctor
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GeneralInfo()
        {
            try
            {
                var model = await _projectService.GetAll(User.GetUserId());
                return PartialView("_GeneralInfo", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Create()
        {
            try
            {
                var model = new ProjectViewModel();

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _projectService.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }

            return opr.ToJSONResult();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _projectService.GetById(id, User.GetUserId());

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _projectService.Edit(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }

            return opr.ToJSONResult();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var opr = OperationResult.Success();
            try
            {
                opr = await _projectService.Delete(id, User.GetUserId());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }
            return opr.ToJSONResult();
        }

    }
}
