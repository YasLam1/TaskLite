using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Projects;
using TaskLite.Application.UseCases.Projects;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly CreateProjectHandler _createProjectHandler;
    private readonly UpdateProjectHandler _updateProjectHandler;
    private readonly DeleteProjectHandler _deleteProjectHandler;

    public ProjectsController(CreateProjectHandler createProjectHandler,
        UpdateProjectHandler updateProjectHandler,
        DeleteProjectHandler deleteProjectHandler)
    {
        _createProjectHandler = createProjectHandler;
        _updateProjectHandler = updateProjectHandler;
        _deleteProjectHandler = deleteProjectHandler;
    }

    [HttpGet]
    public IActionResult GetTest()
    {
        return Ok(new[] { new { Id = 1, Name = "Test Project" } });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest req, CancellationToken ct)
    {
        var project = await _createProjectHandler.HandleAsync(req, ct);

        return CreatedAtAction(nameof(CreateProjectRequest), new { id = project.Id }, project);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProjectRequest req, CancellationToken ct)
    {
        var updated = await _updateProjectHandler.HandleAsync(req, ct);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteProjectHandler.HandleAsync(id, ct);
        return NoContent();
    }

}
