using TaskItem = TaskLite.Domain.Entities.Task;

namespace TaskLite.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<TaskItem> CreateAsync(TaskItem task, CancellationToken ct);
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<TaskItem>> ListByProjectAsync(Guid projectId, CancellationToken ct);
    Task<TaskItem?> UpdateAsync(TaskItem task, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}