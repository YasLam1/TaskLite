using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Comments;

public sealed class ListCommentsByTaskRequest
{
    [Required(ErrorMessage = "TaskId is required.")]
    public Guid TaskId { get; set; }
}