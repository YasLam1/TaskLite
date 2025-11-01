using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Projects;

public sealed class CreateProjectRequest
{
    public Guid OwnerId { get; set; }

    [Required(ErrorMessage = "Project name is required.")]
    [MaxLength(200, ErrorMessage = "Project name cannot exceed 200 characters.")]
    public string Name { get; set; } = string.Empty;
}
