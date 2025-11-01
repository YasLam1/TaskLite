namespace TaskLite.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid AuthorId { get; set; }
    public string Body { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public Task Task { get; set; } = default!;
    public User Author { get; set; } = default!;
}