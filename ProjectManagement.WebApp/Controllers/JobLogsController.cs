using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    [Authorize]
    public class JobLogsController : Controller
    {
        #region Ctor
        private readonly IJobLogService _jobLogService;
        private readonly IJobService _jobService;
        private readonly ILogger<JobLogsController> _logger;

        public JobLogsController(IJobLogService jobLogService, IJobService jobService, ILogger<JobLogsController> logger)
        {
            _jobLogService = jobLogService;
            _jobService = jobService;
            _logger = logger;
        }
        #endregion

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var model = new JobLogIndexViewModel { JobId = id, };
                model.JobCaption = (await _jobService.GetById(id, User.GetUserId())).Caption;
                model.Details = await _jobLogService.GetAll(User.GetUserId(), id);
                model.IsActive = await _jobLogService.IsActive(User.GetUserId(), id);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<OperationResult> Start(JobLogActivityViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();

                opr = await _jobLogService.Start(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }
            return opr;
        }

        [HttpPost]
        public async Task<OperationResult> Stop(JobLogActivityViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();

                opr = await _jobLogService.Stop(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }
            return opr;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var opr = OperationResult.Success();
            try
            {
                opr = await _jobLogService.Delete(id, User.GetUserId());
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
