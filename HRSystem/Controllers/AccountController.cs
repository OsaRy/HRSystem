using HR.DomainModels.Models;
using HR.PresentationModels.Dto.Users;
using HR.Services.Implementation;
using HRSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static HR.Shared.Enums;

namespace HRSystem.Controllers
{
    public class AccountController : Controller
    {
		private readonly SignInManager<EmployeesUsers> _signInManager;
		private readonly UserManager<EmployeesUsers> _userManager; 
		private readonly AccountServices _accountServices;
		public AccountController(SignInManager<EmployeesUsers> signInManager,
			UserManager<EmployeesUsers> UserManager,
			AccountServices AccountServices)
        {
			_signInManager = signInManager;
			_userManager = UserManager;
			_accountServices = AccountServices;

		}

		[HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
           

			returnUrl ??= Url.Content("~/");

			if (User.Identity.IsAuthenticated)
			{
				return LocalRedirect(returnUrl);

			}
			// Clear the existing external cookie to ensure a clean login process
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			var model = new LoginDto();
			model.returnUrl= returnUrl;
			return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			loginDto.returnUrl ??= Url.Content("~/");


			if (ModelState.IsValid)
			{

				
				var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					var user = await _userManager.FindByEmailAsync(loginDto.Email);
					if (user.Emp_ID != 0)
					{
						var employee = await _accountServices.GetEmployee(user);

						HttpContext.Session.SetString("Emp_Name", employee.Name);
						await _accountServices.Check(employee.ID, CheckType.In);
					}
					return LocalRedirect(loginDto.returnUrl);

				}

			}
			ModelState.AddModelError(string.Empty, "Invalid login attempt.");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Logout(string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				
				var user = await _userManager.FindByEmailAsync(User.Identity.Name);
				await _signInManager.SignOutAsync();
				if(user.Emp_ID!=0)
				await _accountServices.Check(user.Emp_ID.Value, CheckType.Out);
				return LocalRedirect(returnUrl);				

			}
			return View();
		}


	}
}
