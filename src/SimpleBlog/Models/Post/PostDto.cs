namespace SimpleBlog.AuthAPI.Models.Post;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorId { get; set; }

    public PostDto(Guid id,
        string title,
        string content,
        DateTime createdAt,
        string authorId)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        AuthorId = authorId;
    }
}