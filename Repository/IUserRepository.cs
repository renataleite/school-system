using Edumin.Models;

namespace Edumin.Repository
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(int userId);
        Task<User> FindByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
