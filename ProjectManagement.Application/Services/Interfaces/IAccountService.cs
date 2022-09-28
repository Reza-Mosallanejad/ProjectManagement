using Microsoft.AspNetCore.Http;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> Register(RegisterViewModel model);

        Task<OperationResult> Login(LoginViewModel model, HttpContext httpContext);

        Task Logout(HttpContext httpContext);

    }
}
