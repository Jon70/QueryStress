using QueryStress.Core.Interfaces;
using QueryStress.Core.Requirements;

namespace QueryStress.Core.LoadProfiles
{
    public class LimitedConcurrencyLoadProfile : IProfile, IExecutionHook
    {
        private readonly int _limit;
        private readonly SemaphoreSlim _semaphore;

        public LimitedConcurrencyLoadProfile(int limit)  
        {
            _limit = limit;
            _semaphore = new SemaphoreSlim(limit);
        }

        public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
        }

        public IRequirement[] Requirements => new IRequirement[] { new ConnectionRequirement(_limit) };

        public Task OnQueryExecutedAsync(ExecutionResult _, CancellationToken cancellationToken)
        {
            _semaphore.Release();

            return Task.CompletedTask;
        }
    }
}
