namespace TaskLite.Application.DTOs.Users;

public sealed class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
