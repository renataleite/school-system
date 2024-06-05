using Edumin.Models;

namespace Edumin.Services
{
	public interface IUserService
	{
		/// <summary>
		/// Registers a new user in the system.
		/// </summary>
		/// <param name="user">The user to register.</param>
		/// <returns>The result of the registration operation.</returns>
		Task<ServiceResult> RegisterUserAsync(User user);

		/// <summary>
		/// Updates information about an existing user.
		/// </summary>
		/// <param name="user">The user with updated information.</param>
		/// <returns>The result of the update operation.</returns>
		Task<ServiceResult> UpdateUserAsync(User user);

		/// <summary>
		/// Finds a user by their email address.
		/// </summary>
		/// <param name="email">The email address to search for.</param>
		/// <returns>The user associated with the email address.</returns>
		Task<User> FindByEmailAsync(string email);

		/// <summary>
		/// Finds a user by their unique identifier.
		/// </summary>
		/// <param name="userId">The unique identifier for the user.</param>
		/// <returns>The user if found; otherwise null.</returns>
		Task<User> FindByIdAsync(int userId);
	}
}
