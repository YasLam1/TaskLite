using TaskLite.Application.Interfaces.Repositories;

namespace TaskLite.Application.UseCases.Users;

public sealed class DeleteUserHandler
{
    private readonly IUserRepository _users;
    public DeleteUserHandler(IUserRepository users) => _users = users;

    public Task HandleAsync(Guid id, CancellationToken ct)
        => _users.DeleteAsync(id, ct);
}
