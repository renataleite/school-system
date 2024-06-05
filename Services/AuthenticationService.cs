using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Edumin.Models;
using Edumin.Repository;

namespace Edumin.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

		public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
		{
			_userRepository = userRepository;
			_configuration = configuration;
		}

		public async Task<AuthenticationResult> AuthenticateUserAsync(User user)
		{
			var existingUser = await _userRepository.FindByEmailAsync(user.Email);
			if (existingUser == null)
			{
				return new AuthenticationResult { Success = false, Message = "Invalid username or password." };
			}

			var verificationResult = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
			if (verificationResult != PasswordVerificationResult.Success)
			{
				return new AuthenticationResult { Success = false, Message = "Invalid login attempt." };
			}

			// Generate JWT Token and return with success
			var tokenString = GenerateJwtToken(existingUser);
			var cookies = new Dictionary<string, string>
		{
			{ "AuthToken", tokenString },
			{ "UserName", existingUser.Username },
			{ "Email", existingUser.Email },
			{ "Role", existingUser.Role }
		};

			return new AuthenticationResult { Success = true, Cookies = cookies };
		}

		private string GenerateJwtToken(User user)
		{
			var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);

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

	public class AuthenticationResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public Dictionary<string, string> Cookies { get; set; }

		public AuthenticationResult()
		{
			Cookies = new Dictionary<string, string>();
		}
	}
}
