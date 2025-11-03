using System.ComponentModel.DataAnnotations;
using TaskStatus = TaskLite.Domain.Enums.TaskStatus;

namespace TaskLite.Application.DTOs.Tasks;

public class UpdateTaskRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public TaskStatus Status { get; set; }
}
