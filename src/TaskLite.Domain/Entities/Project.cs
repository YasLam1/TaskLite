//using Task = TaskLite.Domain.Entities.Task;

namespace TaskLite.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public User Owner { get; set; } = default!;
    public ICollection<User> Participants { get; set; } = [];
    public ICollection<Task> Tasks { get; set; } = [];
}
