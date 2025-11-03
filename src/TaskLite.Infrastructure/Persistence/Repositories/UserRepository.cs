using Microsoft.EntityFrameworkCore;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;
    public UserRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

    public async Task<User> CreateAsync(User user, CancellationToken ct)
    {
        await _appDbContext.Users.AddAsync(user, ct);
        await _appDbContext.SaveChangesAsync(ct);
        return user;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return await _appDbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> UpdateAsync(User user, CancellationToken ct)
    {
        _appDbContext.Users.Update(user);
        await _appDbContext.SaveChangesAsync(ct);
        return user;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _appDbContext.Users.FindAsync([id], ct);
        if (entity != null)
        {
            _appDbContext.Users.Remove(entity);
            await _appDbContext.SaveChangesAsync(ct);
        }
    }
}
