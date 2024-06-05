using Microsoft.AspNetCore.Mvc;
using Edumin.Models;
using Edumin.Services;

namespace Edumin.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IAuthenticationService _authenticationService;

		public AccountController(IUserService userService, IAuthenticationService authenticationService)
		{
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			if (user == null)
			{
				return BadRequest("User data is required.");
			}

			var result = await _userService.RegisterUserAsync(user);
			if (result.Success)
			{
				return RedirectToAction("PageLogin", "Edumin");
			}

			TempData["Error"] = result.Message;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{
			if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
			{
				ModelState.AddModelError("", "Username or password is empty");
				return View();
			}

			var result = await _authenticationService.AuthenticateUserAsync(user);
			if (result.Success)
			{
				foreach (var cookie in result.Cookies)
				{
					HttpContext.Response.Cookies.Append(cookie.Key, cookie.Value, new CookieOptions { HttpOnly = true, Secure = true });
				}
				return RedirectToAction("Index", "Edumin");
			}

			TempData["Error"] = result.Message;
			return RedirectToAction("PageLogin", "Edumin");
		}
	}
}
