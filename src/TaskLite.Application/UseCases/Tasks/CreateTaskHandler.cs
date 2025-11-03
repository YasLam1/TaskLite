using TaskLite.Application.DTOs.Tasks;
using TaskLite.Application.Interfaces.Repositories;
using TaskItem = TaskLite.Domain.Entities.Task;

namespace TaskLite.Application.UseCases.Tasks;

public sealed class CreateTaskHandler
{
    private readonly ITaskRepository _tasks;

    public CreateTaskHandler(ITaskRepository tasks) => _tasks = tasks;

    public async Task<TaskItem> HandleAsync(CreateTaskRequest req, CancellationToken ct)
    {
        TaskItem task = new()
        {
            Id = Guid.NewGuid(),
            ProjectId = req.ProjectId,
            Title = req.Title.Trim(),
            Description = req.Description?.Trim(),
            Status = Domain.Enums.TaskStatus.Todo,
            CreatedAt = DateTime.UtcNow
        };

        return await _tasks.CreateAsync(task, ct);
    }
}