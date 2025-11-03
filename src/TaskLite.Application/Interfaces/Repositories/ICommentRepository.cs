using TaskLite.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment comment, CancellationToken ct);
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Comment>> ListByTaskAsync(Guid taskId, CancellationToken ct);
    Task<Comment?> UpdateAsync(Comment comment, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
