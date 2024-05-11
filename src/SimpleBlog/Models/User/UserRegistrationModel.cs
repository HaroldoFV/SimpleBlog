namespace SimpleBlog.AuthAPI.Models.User;

public class UserRegistrationModel(
    string email,
    string password,
    string confirmPassword
)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string ConfirmPassword { get; set; } = confirmPassword;
}