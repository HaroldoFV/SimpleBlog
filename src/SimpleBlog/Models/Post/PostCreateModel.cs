namespace SimpleBlog.AuthAPI.Models.Post;

public class PostCreateModel(
    string title,
    string content
)
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
}