using TaskLite.Application.DTOs.Users;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Users;

public class GetUserByIdHandler
{
    private readonly IUserRepository _repository;
    public GetUserByIdHandler(IUserRepository repository) => _repository = repository;

    public async Task<User?> HandleAsync(GetUserByIdRequest request, CancellationToken ct)
    {
        return await _repository.GetByIdAsync(request.Id, ct);
    }
}
