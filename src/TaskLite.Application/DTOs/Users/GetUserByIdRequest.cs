using System.ComponentModel.DataAnnotations;

namespace TaskLite.Application.DTOs.Users;

public sealed class GetUserByIdRequest
{
    [Required(ErrorMessage = "User ID is required.")]
    public Guid Id { get; set; }
}
