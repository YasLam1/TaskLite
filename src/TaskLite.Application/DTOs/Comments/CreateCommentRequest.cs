using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Comments;

public sealed class CreateCommentRequest
{
    [Required(ErrorMessage = "TaskId is required.")]
    public Guid TaskId { get; set; }

    [Required(ErrorMessage = "AuthorId is required.")]
    public Guid AuthorId { get; set; }

    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
    public string Content { get; set; } = string.Empty;
}