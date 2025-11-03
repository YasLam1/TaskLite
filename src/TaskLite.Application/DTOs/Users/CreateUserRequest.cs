using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Users;

public sealed class CreateUserRequest
{
    [Required(ErrorMessage = "User name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters.")]
    public string Password { get; set; } = string.Empty;
}
