using SimpleBlog.AuthAPI.Domain.Entity;

namespace SimpleBlog.AuthAPI.Domain.Repository;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password);
}