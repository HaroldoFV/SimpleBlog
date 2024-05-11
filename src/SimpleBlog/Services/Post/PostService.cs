using SimpleBlog.AuthAPI.Domain.Repository;
using SimpleBlog.AuthAPI.Models.Post;

namespace SimpleBlog.AuthAPI.Services.Post;

public class PostService(IPostRepository postRepository) : IPostService
{
    public async Task<PostDto> CreatePostAsync(PostCreateModel model, string userId)
    {
        var post = new Domain.Entity.Post(model.Title, model.Content, userId);

        var createdPost = await postRepository.CreatePostAsync(post);

        return new PostDto
        (
            createdPost.Id,
            createdPost.Title,
            createdPost.Content,
            createdPost.CreatedAt,
            createdPost.AuthorId
        );
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var posts = await postRepository.GetAllPostsAsync();

        return posts.Select(p => new PostDto
        (
            p.Id,
            p.Title,
            p.Content,
            p.CreatedAt,
            p.AuthorId
        ));
    }

    public async Task<PostDto> GetPostByIdAsync(Guid id)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        return new PostDto(
            post.Id,
            post.Title,
            post.Content,
            post.CreatedAt,
            post.AuthorId
        );
    }

    public async Task<bool> UpdatePostAsync(Guid id, PostUpdateModel model, string userId)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        if (post.AuthorId == userId)
        {
            post.Title = model.Title;
            post.Content = model.Content;
            return await postRepository.UpdatePostAsync(post);
        }

        return false;
    }

    public async Task<bool> DeletePostAsync(Guid id, string userId)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        if (post.AuthorId == userId)
        {
            return await postRepository.DeletePostAsync(id);
        }

        return false;
    }
}