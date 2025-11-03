using TaskLite.Domain.Entities;

namespace TaskLite.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task<User> CreateAsync(User user, CancellationToken ct);
    Task<User> UpdateAsync(User user, CancellationToken ct);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken ct);
}
