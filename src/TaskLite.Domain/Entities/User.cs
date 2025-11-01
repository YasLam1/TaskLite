namespace TaskLite.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public ICollection<Project> ProjectsOwned { get; set; } = [];
    public ICollection<Project> ParticipatingProjects { get; set; } = [];
    public ICollection<Task> TasksAssigned { get; set; } = [];
    public ICollection<Comment> CommentsAuthored { get; set; } = [];
}
