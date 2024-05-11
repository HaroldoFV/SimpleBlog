using Microsoft.AspNetCore.Identity;

namespace SimpleBlog.AuthAPI.Domain.Entity;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    // private ApplicationUser()
    // {
    // }
}