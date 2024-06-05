using Edumin.Models;
using Edumin.Repository;
using Microsoft.AspNetCore.Identity;

namespace Edumin.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		public async Task<ServiceResult> RegisterUserAsync(User user)
		{
			if (user == null)
			{
				return ServiceResult.Failure("User data must not be null.");
			}

			try
			{
				// Hash the password
				user.Password = _passwordHasher.HashPassword(user, user.Password);

				// Add the user to the repository
				await _userRepository.AddAsync(user);

				return ServiceResult.Successful("User registered successfully.");
			}
			catch (Exception ex)
			{
				// Log the exception details here using your preferred logging framework
				return ServiceResult.Failure($"Error registering user: {ex.Message}");
			}
		}

		public async Task<ServiceResult> UpdateUserAsync(User user)
		{
			if (user == null)
			{
				return ServiceResult.Failure("User data must not be null.");
			}

			try
			{
				// Update the user in the repository
				await _userRepository.UpdateAsync(user);

				return ServiceResult.Successful("User updated successfully.");
			}
			catch (Exception ex)
			{
				// Log the exception details here
				return ServiceResult.Failure($"Error updating user: {ex.Message}");
			}
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
			}

			return await _userRepository.FindByEmailAsync(email);
		}

		public async Task<User> FindByIdAsync(int userId)
		{
			return await _userRepository.FindByIdAsync(userId);
		}
	}

	public class ServiceResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		public ServiceResult(bool success, string message)
		{
			Success = success;
			Message = message;
		}

		public static ServiceResult Successful(string message = "Operation successful.")
		{
			return new ServiceResult(true, message);
		}

		public static ServiceResult Failure(string message)
		{
			return new ServiceResult(false, message);
		}
	}

}
