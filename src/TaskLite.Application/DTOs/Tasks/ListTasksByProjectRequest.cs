using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Tasks;

public sealed class ListTasksByProjectRequest
{
    [Required(ErrorMessage = "ProjectId is required.")]
    public Guid ProjectId { get; set; }
}
