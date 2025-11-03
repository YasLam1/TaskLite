using TaskLite.Application.DTOs.Comments;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Comments;

public sealed class ListCommentsByTaskHandler
{
    private readonly ICommentRepository _comments;

    public ListCommentsByTaskHandler(ICommentRepository comments)
    {
        _comments = comments;
    }

    public async Task<IReadOnlyList<Comment>> HandleAsync(ListCommentsByTaskRequest req, CancellationToken ct)
    {
        return await _comments.ListByTaskAsync(req.TaskId, ct);
    }
}