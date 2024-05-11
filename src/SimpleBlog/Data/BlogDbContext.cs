using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.AuthAPI.Domain.Entity;

namespace SimpleBlog.AuthAPI.Data;

public class BlogDbContext(
    DbContextOptions<BlogDbContext> options
)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Content).IsRequired();

            entity.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired();
        });
    }
}