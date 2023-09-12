using QueryStress.Core.Interfaces;

namespace QueryStress.Core.LoadProfiles;

public class SequentialLoadProfile : IProfile, IExecutionHook
{
    private TaskCompletionSource? _taskCompletionSource;

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
    {
        if (_taskCompletionSource != null)
        {
            await _taskCompletionSource.Task;
        }

        _taskCompletionSource = new TaskCompletionSource();
    }

    public Task OnQueryExecutedAsync(ExecutionResult _, CancellationToken cancellationToken)
    {
        if (_taskCompletionSource == null)
        {
            throw new InvalidOperationException($"{nameof(OnQueryExecutedAsync)}");
        }

        _taskCompletionSource.SetResult();

        return Task.CompletedTask;
    }
}