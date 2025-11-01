using Microsoft.EntityFrameworkCore;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

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
}
