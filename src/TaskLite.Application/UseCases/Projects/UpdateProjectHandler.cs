using TaskLite.Application.DTOs.Projects;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Projects;

public class UpdateProjectHandler
{
    private readonly IProjectRepository _projects;
    public UpdateProjectHandler(IProjectRepository projects)
        => _projects = projects;

    public async Task<Project?> HandleAsync(UpdateProjectRequest req, CancellationToken ct)
    {
        var project = await _projects.GetByIdAsync(req.Id, ct);
        if (project == null) return null;

        if (req.Name != null) project.Name = req.Name;

        return await _projects.UpdateAsync(project, ct);
    }
}
