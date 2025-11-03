using TaskLite.Application.DTOs.Comments;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Comments;

public sealed class UpdateCommentHandler
{
    private readonly ICommentRepository _comments;

    public UpdateCommentHandler(ICommentRepository comments) => _comments = comments;

    public async Task<Comment?> HandleAsync(UpdateCommentRequest req, CancellationToken ct)
    {
        var comment = await _comments.GetByIdAsync(req.Id, ct);
        if (comment is null) return null;

        if (req.Content != null) comment.Body = req.Content.Trim();

        return await _comments.UpdateAsync(comment, ct);
    }
}