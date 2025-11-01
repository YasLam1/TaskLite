using TaskLite.Application.DTOs.Projects;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Projects;

public class CreateProjectHandler
{
    private readonly IProjectRepository _repository;
    public CreateProjectHandler(IProjectRepository repository) => _repository = repository;

    public async Task<Project> HandleAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var name = request.Name.Trim();
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty.");

        Project project = new()
        {
            Id = Guid.NewGuid(),
            OwnerId = request.OwnerId,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(project, ct);
    }
}
