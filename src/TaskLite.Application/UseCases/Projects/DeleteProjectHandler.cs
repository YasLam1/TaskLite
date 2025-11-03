using TaskLite.Application.Interfaces.Repositories;

namespace TaskLite.Application.UseCases.Projects;

public class DeleteProjectHandler
{
    private readonly IProjectRepository _projects;
    public DeleteProjectHandler(IProjectRepository projects)
        => _projects = projects;

    public Task HandleAsync(Guid id, CancellationToken ct)
        => _projects.DeleteAsync(id, ct);
}
