using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Projects;

public sealed class ListProjectsByOwnerRequest
{
    [Required(ErrorMessage = "OwnerId is required.")]
    public Guid OwnerId { get; set; }
}