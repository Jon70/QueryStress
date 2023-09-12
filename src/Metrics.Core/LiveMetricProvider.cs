using QueryStress.Core;
using QueryStress.Core.Interfaces;

namespace Metrics.Core
{
    public class LiveMetricProvider : ILiveMetricProvider
    {
        private int _count;
        private long _ticks;

        public Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken)
        {
            Interlocked.Add(ref _ticks, result.Duration.Ticks);
            Interlocked.Increment(ref _count);

            return Task.CompletedTask;
        }

        public IEnumerable<IMetric> GetMetrics()
        {
            yield return new SimpleMetric("Mean",
                _count == 0 ? TimeSpan.Zero : new TimeSpan(_ticks / _count));
            yield return new SimpleMetric("Count", _count);
        }
    }
}
