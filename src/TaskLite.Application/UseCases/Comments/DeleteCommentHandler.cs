using TaskLite.Application.Interfaces.Repositories;

namespace TaskLite.Application.UseCases.Comments;

public sealed class DeleteCommentHandler
{
    private readonly ICommentRepository _comments;
    public DeleteCommentHandler(ICommentRepository comments) => _comments = comments;

    public async Task HandleAsync(Guid id, CancellationToken ct)
    {
        await _comments.DeleteAsync(id, ct);
    }
}