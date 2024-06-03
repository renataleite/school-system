using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Edumin.Models;
using Edumin.Context;

namespace Edumin.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolContext _context;
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();


        public AccountController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult PageLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                user.Password = passwordHasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Edumin");

            }
            catch (Exception ex)
            {

                throw new Exception("Failed to save user.", ex);
            }

        }

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{
			if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
			{
				ModelState.AddModelError("", "Username or password is empty");
				return View();
			}

			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (existingUser == null)
			{
				TempData["Error"] = "Usuário ou senha inválido.";
				return RedirectToAction("PageLogin", "Edumin");
			}


			var verificationResult = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
			if (verificationResult == PasswordVerificationResult.Failed)
			{
				ModelState.AddModelError("", "Invalid login attempt.");
				return View();
			}

			var tokenString = GenerateJwtToken(existingUser); // Assume GenerateJwtToken faz todo o processo de criar o token.

			HttpContext.Response.Cookies.Append("AuthToken", tokenString, new CookieOptions { HttpOnly = true, Secure = true });
			HttpContext.Response.Cookies.Append("UserName", existingUser.Username, new CookieOptions { HttpOnly = true, Secure = true });
			HttpContext.Response.Cookies.Append("Email", existingUser.Email, new CookieOptions { HttpOnly = true, Secure = true });

			return RedirectToAction("Index", "Edumin");
		}

		private string GenerateJwtToken(User user)
		{
			var key = Encoding.ASCII.GetBytes("3m4qWrmPhdVQ/hXA78j+8bBTIZr0ceGO02fOiT/z/As=\r\n");
			var securityKey = new SymmetricSecurityKey(key);
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
			new Claim("userId", user.UserId.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
		}),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = credentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
