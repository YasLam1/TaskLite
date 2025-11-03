using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Tasks;
using TaskLite.Application.UseCases.Tasks;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TasksController : ControllerBase
{
    private readonly CreateTaskHandler _createTaskHandler;
    private readonly ListTasksByProjectHandler _listTasksHandler;
    private readonly UpdateTaskHandler _updateTaskHandler;
    private readonly DeleteTaskHandler _deleteTaskHandler;

    public TasksController(
        CreateTaskHandler createTaskHandler,
        ListTasksByProjectHandler listTasksHandler,
        UpdateTaskHandler updateTaskHandler,
        DeleteTaskHandler deleteTaskHandler)
    {
        _createTaskHandler = createTaskHandler;
        _listTasksHandler = listTasksHandler;
        _updateTaskHandler = updateTaskHandler;
        _deleteTaskHandler = deleteTaskHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest req, CancellationToken ct)
    {
        var task = await _createTaskHandler.HandleAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> ListByProject(Guid projectId, CancellationToken ct)
    {
        var req = new ListTasksByProjectRequest { ProjectId = projectId };
        var tasks = await _listTasksHandler.HandleAsync(req, ct);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id) => Ok();

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateTaskRequest req, CancellationToken ct)
    {
        var updated = await _updateTaskHandler.HandleAsync(req, ct);
        if (updated is null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteTaskHandler.HandleAsync(id, ct);
        return NoContent();
    }
}
