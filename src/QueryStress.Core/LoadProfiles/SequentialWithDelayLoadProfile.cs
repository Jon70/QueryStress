using QueryStress.Core.Interfaces;

namespace QueryStress.Core.LoadProfiles;

public class SequentialWithDelayLoadProfile : IProfile, IExecutionHook
{
    private readonly TimeSpan _delay;
    private readonly SequentialLoadProfile _sequentialLoadProfile;
    private DateTime? _nextExecution;

    public SequentialWithDelayLoadProfile(TimeSpan delay)
    {
        _delay = delay;
        _sequentialLoadProfile = new SequentialLoadProfile();
    }

    public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
    {
        await _sequentialLoadProfile.WhenNextCanBeExecutedAsync(cancellationToken);

        var now = DateTime.Now;

        if (now < _nextExecution)
        {
            await Task.Delay(_nextExecution.Value - now, cancellationToken);
        }
    }

    public async Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken)
    {
        await _sequentialLoadProfile.OnQueryExecutedAsync(result, cancellationToken);
        _nextExecution = DateTime.Now + _delay;
    }
}