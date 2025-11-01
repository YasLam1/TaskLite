using TaskLite.Application.DTOs.Projects;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Projects;

public class ListProjectsByOwnerHandler
{
    private readonly IProjectRepository _repository;
    public ListProjectsByOwnerHandler(IProjectRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<Project>> HandleAsync(ListProjectsByOwnerRequest request, CancellationToken ct)
    {
        if (request.OwnerId == Guid.Empty)
            throw new ArgumentException("OwnerId is required.", nameof(request.OwnerId));

        return await _repository.ListByOwnerAsync(request.OwnerId, ct);
    }
}
