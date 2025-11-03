using TaskLite.Application.DTOs.Tasks;
using TaskLite.Application.Interfaces.Repositories;
using TaskItem = TaskLite.Domain.Entities.Task;

namespace TaskLite.Application.UseCases.Tasks;

public sealed class ListTasksByProjectHandler
{
    private readonly ITaskRepository _tasks;
    public ListTasksByProjectHandler(ITaskRepository tasks) => _tasks = tasks;

    public async Task<IReadOnlyList<TaskItem>> HandleAsync(ListTasksByProjectRequest req, CancellationToken ct)
    {
        return await _tasks.ListByProjectAsync(req.ProjectId, ct);
    }
}
