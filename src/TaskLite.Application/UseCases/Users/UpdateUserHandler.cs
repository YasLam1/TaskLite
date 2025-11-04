using TaskLite.Application.DTOs.Users;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Users;

public sealed class UpdateUserHandler
{
    private readonly IUserRepository _users;
    public UpdateUserHandler(IUserRepository users) => _users = users;

    public async Task<User?> HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        var user = await _users.GetByIdAsync(req.Id, ct);
        if (user is null) return null;

        if (req.Name != null) user.FullName = req.Name.Trim();
        if (req.Email != null) user.Email = req.Email.Trim().ToLowerInvariant();

        return await _users.UpdateAsync(user, ct);
    }
}
