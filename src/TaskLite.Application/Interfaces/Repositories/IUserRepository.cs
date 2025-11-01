using TaskLite.Domain.Entities;

namespace TaskLite.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task<User> CreateAsync(User user, CancellationToken ct);
}
