using Edumin.Models;
using static Edumin.Services.AuthenticationService;

namespace Edumin.Services
{
	public interface IAuthenticationService
	{
		Task<AuthenticationResult> AuthenticateUserAsync(User user);
	}

}
