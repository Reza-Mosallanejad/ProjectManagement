using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Services.Interfaces;
using ProjectManagement.Application.Utilities;
using ProjectManagement.Application.ViewModels;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using System.Security.Claims;

namespace ProjectManagement.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Register(RegisterViewModel model)
        {
            var op = ViewModelValidator(model);

            if (op.Status)
            {
                var entity = new User
                {
                    FullName = model.FullName,
                    Username = model.Username,
                    Password = SecurityUtils.GenerateHash(model.Password),
                    Email = model.Email,
                };
                op = await _userRepository.AddAsync(entity);
                op = await _userRepository.SaveAsync(op);
            }

            return op;
        }

        private OperationResult ViewModelValidator(RegisterViewModel model)
        {
            return OperationResult.Success();
        }

        public async Task<OperationResult> Login(LoginViewModel model, HttpContext httpContext)
        {
            var opr = OperationResult.Success();

            var user = await _userRepository.All.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == SecurityUtils.GenerateHash(model.Password));

            if (user != null)
            {
                //login
                var claims = new List<Claim>
               {
                   new Claim("Id", user.Id.ToString()),
                   new Claim(ClaimTypes.NameIdentifier, user.Username),
                   new Claim(ClaimTypes.Name, user.FullName),
               };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await httpContext.SignInAsync(principal, properties);
                // 
            }
            else
            {
                opr = OperationResult.Fail("Login info is invalid.");
            }

            return opr;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
