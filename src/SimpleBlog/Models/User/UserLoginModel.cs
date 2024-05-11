namespace SimpleBlog.AuthAPI.Models.User;

public class UserLoginModel(
    string email,
    string password
)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}