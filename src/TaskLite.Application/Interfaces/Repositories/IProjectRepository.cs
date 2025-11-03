using TaskLite.Domain.Entities;

namespace TaskLite.Application.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project, CancellationToken ct);
    Task<Project?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Project>> ListByOwnerAsync(Guid ownerId, CancellationToken ct);
    Task<Project> UpdateAsync(Project project, CancellationToken ct);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken ct);
}
