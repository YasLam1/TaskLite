using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Projects;
using TaskLite.Application.UseCases.Projects;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : Controller
{
    private readonly CreateProjectHandler _createProjectHandler;

    public ProjectsController(CreateProjectHandler createProjectHandler) 
        => _createProjectHandler = createProjectHandler;

    [HttpPost("api/projects")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest req, CancellationToken ct)
    {
        var project = await _createProjectHandler.HandleAsync(req, ct);

        return CreatedAtAction(nameof(CreateProjectRequest), new { id = project.Id }, project);
    }

    //[HttpGet("api/projects/owner/{ownerId}")]
    //public async Task<IActionResult> List(Guid ownerId, CancellationToken ct)
    //{

    //}
}
