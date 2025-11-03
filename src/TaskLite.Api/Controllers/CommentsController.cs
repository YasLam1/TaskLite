using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Comments;
using TaskLite.Application.UseCases.Comments;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CommentsController : ControllerBase
{
    private readonly CreateCommentHandler _createCommentHandler;
    private readonly ListCommentsByTaskHandler _listCommentsHandler;
    private readonly UpdateCommentHandler _updateCommentHandler;
    private readonly DeleteCommentHandler _deleteCommentHandler;

    public CommentsController(
        CreateCommentHandler createCommentHandler,
        ListCommentsByTaskHandler listCommentsHandler,
        UpdateCommentHandler updateCommentHandler,
        DeleteCommentHandler deleteCommentHandler)
    {
        _createCommentHandler = createCommentHandler;
        _listCommentsHandler = listCommentsHandler;
        _updateCommentHandler = updateCommentHandler;
        _deleteCommentHandler = deleteCommentHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest req, CancellationToken ct)
    {
        var comment = await _createCommentHandler.HandleAsync(req, ct);
        return CreatedAtAction(nameof(ListByTask), new { taskId = comment.TaskId }, comment);
    }

    [HttpGet("task/{taskId:guid}")]
    public async Task<IActionResult> ListByTask(Guid taskId, CancellationToken ct)
    {
        var req = new ListCommentsByTaskRequest { TaskId = taskId };
        var comments = await _listCommentsHandler.HandleAsync(req, ct);
        return Ok(comments);
    }

    public async Task<IActionResult> Update([FromBody] UpdateCommentRequest req, CancellationToken ct)
    {
        var updated = await _updateCommentHandler.HandleAsync(req, ct);
        if (updated is null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteCommentHandler.HandleAsync(id, ct);
        return NoContent();
    }
}
