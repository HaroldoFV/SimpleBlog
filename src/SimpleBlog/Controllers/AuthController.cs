using Microsoft.AspNetCore.Mvc;
using SimpleBlog.AuthAPI.Models.User;
using SimpleBlog.AuthAPI.Services.User;

namespace SimpleBlog.AuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Password != model.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var result = await _authService.RegisterUserAsync(model);

            if (result.Succeeded)
                return Ok("User registered successfully");

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await _authService.AuthenticateUserAsync(model);

            if (!string.IsNullOrEmpty(token))
                return Ok(new {Token = token});

            return Unauthorized("Invalid credentials");
        }

        return BadRequest(ModelState);
    }
}