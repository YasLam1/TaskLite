using Microsoft.EntityFrameworkCore;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _appDbContext;
    public ProjectRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public async Task<Project> CreateAsync(Project project, CancellationToken ct)
    {
        await _appDbContext.Projects.AddAsync(project, ct);
        await _appDbContext.SaveChangesAsync(ct);
        return project;
    }

    public Task<Project?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _appDbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public async Task<IReadOnlyList<Project>> ListByOwnerAsync(Guid ownerId, CancellationToken ct)
    {
        return await _appDbContext.Projects
            .AsNoTracking()
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync(ct);
    }

    public async Task<Project> UpdateAsync(Project project, CancellationToken ct)
    {
        _appDbContext.Projects.Update(project);
        await _appDbContext.SaveChangesAsync(ct);
        return project;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _appDbContext.Projects.FindAsync([id], ct);
        if (entity != null)
        {
            _appDbContext.Projects.Remove(entity);
            await _appDbContext.SaveChangesAsync(ct);
        }
    }
}
