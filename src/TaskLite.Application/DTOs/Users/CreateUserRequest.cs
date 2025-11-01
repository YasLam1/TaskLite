using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Users;

public sealed class CreateUserRequest
{
    [Required(ErrorMessage = "User name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
    public string Email { get; set; } = string.Empty;
}
