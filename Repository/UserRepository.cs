using Edumin.Context;
using Edumin.Models;
using Microsoft.EntityFrameworkCore;


namespace Edumin.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly SchoolContext _context;

		public UserRepository(SchoolContext context)
		{
			_context = context;
		}

		public async Task<User> FindByIdAsync(int userId)
		{
			return await _context.Users.FindAsync(userId);
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task AddAsync(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(User user)
		{
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}
	}
}
