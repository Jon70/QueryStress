using QueryStress.Core.Interfaces;

namespace QueryStress.Core.LoadProfiles
{
    public class LimitedConcurrencyWithDelayLoadProfile : IProfile, IExecutionHook
    {
        private readonly LimitedConcurrencyLoadProfile _internal;
        private readonly TimeSpan _delay;

        public LimitedConcurrencyWithDelayLoadProfile(int limit, TimeSpan delay)
        {
            _delay = delay;
            _internal = new LimitedConcurrencyLoadProfile(limit);
        }

        public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
        {
            await _internal.WhenNextCanBeExecutedAsync(CancellationToken.None);
        }

        public async Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken)
        {
            await Task.Delay(_delay, cancellationToken);
            await _internal.OnQueryExecutedAsync(result, cancellationToken);
        }
    }
}
