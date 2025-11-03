using TaskLite.Application.Interfaces.Repositories;

namespace TaskLite.Application.UseCases.Tasks;

public sealed class DeleteTaskHandler
{
    private readonly ITaskRepository _tasks;

    public DeleteTaskHandler(ITaskRepository tasks) => _tasks = tasks;

    public async Task HandleAsync(Guid id, CancellationToken ct)
    {
        await _tasks.DeleteAsync(id, ct);
    }
}