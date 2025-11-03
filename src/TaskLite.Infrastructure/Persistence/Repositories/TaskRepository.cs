using Microsoft.EntityFrameworkCore;
using TaskItem = TaskLite.Domain.Entities.Task;
using TaskLite.Application.Interfaces.Repositories;

namespace TaskLite.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;
    public TaskRepository(AppDbContext db) => _db = db;

    public async Task<TaskItem> CreateAsync(TaskItem task, CancellationToken ct)
    {
        await _db.Tasks.AddAsync(task, ct);
        await _db.SaveChangesAsync(ct);
        return task;
    }

    public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Tasks
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<IReadOnlyList<TaskItem>> ListByProjectAsync(Guid projectId, CancellationToken ct)
    {
        var tasks = await _db.Tasks
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .ToListAsync(ct);

        return tasks;
    }

    public async Task<TaskItem?> UpdateAsync(TaskItem task, CancellationToken ct)
    {
        _db.Tasks.Update(task);
        await _db.SaveChangesAsync(ct);
        return task;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Tasks.FindAsync([id], ct);
        if (entity != null)
        {
            _db.Tasks.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}