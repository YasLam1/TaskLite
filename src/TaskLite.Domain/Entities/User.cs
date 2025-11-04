using Microsoft.AspNetCore.Identity;

namespace TaskLite.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;

    // navigation
    public ICollection<Project> ProjectsOwned { get; set; } = [];
    public ICollection<Project> ParticipatingProjects { get; set; } = [];
    public ICollection<Task> TasksAssigned { get; set; } = [];
    public ICollection<Comment> CommentsAuthored { get; set; } = [];
}
