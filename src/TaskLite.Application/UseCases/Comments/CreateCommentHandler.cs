using TaskLite.Application.DTOs.Comments;
using TaskLite.Application.Interfaces.Repositories;
using TaskLite.Domain.Entities;

namespace TaskLite.Application.UseCases.Comments;

public sealed class CreateCommentHandler
{
    private readonly ICommentRepository _comments;

    public CreateCommentHandler(ICommentRepository comments)
    {
        _comments = comments;
    }

    public async Task<Comment> HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        Comment comment = new()
        {
            Id = Guid.NewGuid(),
            TaskId = req.TaskId,
            AuthorId = req.AuthorId,
            Body = req.Content.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return await _comments.CreateAsync(comment, ct);
    }
}
