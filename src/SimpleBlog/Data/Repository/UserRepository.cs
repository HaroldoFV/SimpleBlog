using Microsoft.AspNetCore.Identity;
using SimpleBlog.AuthAPI.Domain.Entity;
using SimpleBlog.AuthAPI.Domain.Repository;

namespace SimpleBlog.AuthAPI.Data.Repository;

public class UserRepository(
    UserManager<ApplicationUser> userManager
)
    : IUserRepository
{
    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId) ??
               throw new Exception($"User '{userId}' not found.");
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password)
    {
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return user;
        }

        var errors = string
            .Join(
                ", ",
                result.Errors.Select(e => e.Description)
            );
        throw new InvalidOperationException($"Falha ao criar usuário: {errors}");
    }
}