using SimpleBlog.AuthAPI.Models.Post;

namespace SimpleBlog.AuthAPI.Services.Post;

public interface IPostService
{
    Task<PostDto> CreatePostAsync(PostCreateModel model, string userId);
    Task<IEnumerable<PostDto>> GetAllPostsAsync();
    Task<PostDto> GetPostByIdAsync(Guid id);
    Task<bool> UpdatePostAsync(Guid id, PostUpdateModel model, string userId);
    Task<bool> DeletePostAsync(Guid id, string userId);
}