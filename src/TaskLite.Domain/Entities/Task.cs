using TaskStatus = TaskLite.Domain.Enums.TaskStatus;

namespace TaskLite.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime? DueDate { get; set; }

    // foreign keys
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public Project Project { get; set; } = default!;
    public User? Assignee { get; set; }
    public ICollection<Comment> Comments { get; set; } = [];
}
