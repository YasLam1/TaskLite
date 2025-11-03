using TaskLite.Application.DTOs.Tasks;
using TaskLite.Application.Interfaces.Repositories;
using TaskItem = TaskLite.Domain.Entities.Task;

namespace TaskLite.Application.UseCases.Tasks;

public sealed class UpdateTaskHandler
{
    private readonly ITaskRepository _tasks;

    public UpdateTaskHandler(ITaskRepository tasks) => _tasks = tasks;

    public async Task<TaskItem?> HandleAsync(UpdateTaskRequest req, CancellationToken ct)
    {
        var task = await _tasks.GetByIdAsync(req.Id, ct);
        if (task is null) return null;

        if (task.Title != null) task.Title = req.Title.Trim();
        if (task.Description != null) task.Description = req.Description?.Trim();
        if (task.Status != req.Status) task.Status = req.Status;

        return await _tasks.UpdateAsync(task, ct);
    }
}
