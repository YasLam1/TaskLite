using Microsoft.EntityFrameworkCore;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.Infrastructure.Persistence.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _db;
    public CommentRepository(AppDbContext db) => _db = db;

    public async Task<Comment> CreateAsync(Comment comment, CancellationToken ct)
    {
        await _db.Comments.AddAsync(comment, ct);
        await _db.SaveChangesAsync(ct);
        return comment;
    }

    public Task<Comment?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Comment>> ListByTaskAsync(Guid taskId, CancellationToken ct)
    {
        var comments = await _db.Comments
                               .AsNoTracking()
                               .Where(c => c.TaskId == taskId)
                               .OrderByDescending(c => c.CreatedAt)
                               .ToListAsync(ct);
        return comments;
    }

    public async Task<Comment?> UpdateAsync(Comment comment, CancellationToken ct)
    {
        _db.Comments.Update(comment);
        await _db.SaveChangesAsync(ct);
        return comment;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Comments.FindAsync([id], ct);
        if (entity != null)
        {
            _db.Comments.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}