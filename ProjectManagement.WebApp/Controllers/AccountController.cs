using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.WebApp.Utilities;

namespace ProjectManagement.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, ILogger<ProjectsController> logger)
        {
            _logger = logger;
            _accountService = accountService;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _accountService.Register(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }

            if (opr.Status)
                return RedirectToAction("Login");
            else
                return opr.ToJSONResult();
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var opr = OperationResult.Success();
            try
            {
                opr = ModelState.IsValid();

                if (opr.Status)
                    opr = await _accountService.Login(model, HttpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                opr = OperationResult.Fail();
            }

            return opr.ToJSONResult();
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _accountService.Logout(HttpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}
