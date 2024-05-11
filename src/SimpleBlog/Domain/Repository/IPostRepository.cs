using SimpleBlog.AuthAPI.Domain.Entity;

namespace SimpleBlog.AuthAPI.Domain.Repository;

public interface IPostRepository
{
    Task<Post> GetPostByIdAsync(Guid id);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<Post> CreatePostAsync(Post post);
    Task<bool> UpdatePostAsync(Post post);
    Task<bool> DeletePostAsync(Guid id);
}