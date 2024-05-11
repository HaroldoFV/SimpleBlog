namespace SimpleBlog.AuthAPI.Domain.Entity;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public ApplicationUser Author { get; set; } = new();

    public Post(
        string title,
        string content,
        string authorId
    )
    {
        Id = Guid.NewGuid();
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now.ToUniversalTime();
        AuthorId = authorId;

        Validate();
    }

    public void Upate(
        string title,
        string content
    )

    {
        Title = title;
        Content = content;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrEmpty(Title))
            throw new Exception($"{nameof(Title)} should not be empty or null.");

        if (string.IsNullOrEmpty(Content))
            throw new Exception($"{nameof(Content)} should not be empty or null.");
    }
}