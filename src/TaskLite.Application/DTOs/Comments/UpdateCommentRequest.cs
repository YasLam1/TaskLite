using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Comments;

public sealed class UpdateCommentRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters.")]
    public string Content { get; set; } = string.Empty;
}