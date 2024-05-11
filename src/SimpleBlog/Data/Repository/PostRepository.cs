using Microsoft.EntityFrameworkCore;
using SimpleBlog.AuthAPI.Domain.Entity;
using SimpleBlog.AuthAPI.Domain.Repository;

namespace SimpleBlog.AuthAPI.Data.Repository;

public class PostRepository(BlogDbContext context)
    : IPostRepository
{
    public async Task<Post> GetPostByIdAsync(Guid id)
    {
        return await context
                   .Posts
                   .Include(p => p.Author)
                   .FirstOrDefaultAsync(p => p.Id == id) ??
               throw new Exception($"User '{id}' not found.");
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await context
            .Posts
            .Include(p => p.Author)
            .ToListAsync();
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }

    public async Task<bool> UpdatePostAsync(Post post)
    {
        context.Posts.Update(post);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePostAsync(Guid id)
    {
        var post = await GetPostByIdAsync(id);

        context.Posts.Remove(post);
        return await context.SaveChangesAsync() > 0;
    }
}