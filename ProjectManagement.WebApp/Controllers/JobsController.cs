using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        #region Ctor
        private readonly IJobService _jobService;
        private readonly IProjectService _projectService;
        private readonly ISplitService _splitService;
        private readonly ILogger<JobsController> _logger;

        public JobsController(IJobService jobService, IProjectService projectService, ISplitService splitService, ILogger<JobsController> logger)
        {
            _jobService = jobService;
            _projectService = projectService;
            _splitService = splitService;
            _logger = logger;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new Dictionary<int, string>();
                model.Add(0, "-Select an item-");
                var projects = await _projectService.GetAll(User.GetUserId());
                foreach (var project in projects)
                    model.Add(project.Id, project.Caption);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> GetJobs(int projectId, int splitId, bool active)
        {
            try
            {
                var model = await _jobService.GetAllInfo(User.GetUserId(), projectId, splitId);

                if (active)
                    model = model.Where(m => m.IsActive).ToList();

                return PartialView("_Jobs", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> GetSplitFilter(int id)
        {
            try
            {
                var splits = new List<SplitViewModel>();

                if (id > 0)
                    splits = await _splitService.GetAll(User.GetUserId(), id);

                var model = splits.Select(s => new
                {
                    s.Id,
                    s.Caption
                });

                return Json(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IActionResult Create(int id)
        {
            try
            {
                var model = new JobViewModel() { SplitId = id };

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _jobService.Create(model);
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
                var model = await _jobService.GetById(id, User.GetUserId());

                return PartialView("Form", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                model.UserId = User.GetUserId();
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _jobService.Edit(model);
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
                opr = await _jobService.Delete(id, User.GetUserId());
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
