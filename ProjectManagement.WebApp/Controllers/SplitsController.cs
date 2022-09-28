using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    [Authorize]
    public class SplitsController : Controller
    {
        #region Ctor
        private readonly ISplitService _splitService;
        private readonly IProjectService _projectService;
        private readonly ILogger<SplitsController> _logger;

        public SplitsController(ISplitService splitService, IProjectService projectService, ILogger<SplitsController> logger)
        {
            _splitService = splitService;
            _projectService = projectService;
            _logger = logger;
        }
        #endregion

        public async Task<IActionResult> GeneralInfo(int id)
        {
            try
            {
                var model = new SplitGeneralInfoViewModel { ProjectId = id, };
                model.ProjectCaption = (await _projectService.GetById(id, User.GetUserId())).Caption;

                model.Details = await _splitService.GetAll(User.GetUserId(), id);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Create(int projectId)
        {
            try
            {
                var model = new SplitViewModel { ProjectId = projectId };

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SplitViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _splitService.Create(model);
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
                var model = await _splitService.GetById(id, User.GetUserId());

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SplitViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _splitService.Edit(model);
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
                opr = await _splitService.Delete(id, User.GetUserId());
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
