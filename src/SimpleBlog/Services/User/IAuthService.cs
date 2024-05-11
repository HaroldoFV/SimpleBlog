using Microsoft.AspNetCore.Identity;
using SimpleBlog.AuthAPI.Models.User;

namespace SimpleBlog.AuthAPI.Services.User;

public interface IAuthService
{
    Task<string> AuthenticateUserAsync(UserLoginModel model);
    Task<IdentityResult> RegisterUserAsync(UserRegistrationModel model);
}