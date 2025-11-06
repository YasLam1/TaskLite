using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Users;

public sealed class UpdateUserRequest
{
    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;
}
