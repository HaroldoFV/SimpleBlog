using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SimpleBlog.AuthAPI.Domain.Entity;
using SimpleBlog.AuthAPI.Models.User;

namespace SimpleBlog.AuthAPI.Services.User;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration
) : IAuthService
{
    public async Task<string> AuthenticateUserAsync(UserLoginModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);

        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            return token;
        }

        return string.Empty;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationModel model)
    {
        var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
        var result = await userManager.CreateAsync(user, model.Password);
        return result;
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JwtKey"] ?? throw new InvalidOperationException())
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        if (user.UserName != null)
        {
            if (user.Email != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email), new Claim("uid", user.Id)
                };

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        return string.Empty;
    }
}