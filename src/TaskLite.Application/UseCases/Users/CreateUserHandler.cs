using TaskLite.Application.DTOs.Users;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Users;

public class CreateUserHandler
{
    private readonly IUserRepository _repository;
    public CreateUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<User> HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
            throw new ArgumentNullException(nameof(request));

        if (await _repository.EmailExistsAsync(request.Email, ct))
            throw new InvalidOperationException("Email is already in use.");

        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        return await _repository.CreateAsync(user, ct);
    }
}
