using Perfolizer.Mathematics.Common;
using Perfolizer.Mathematics.QuantileEstimators;
using QueryStress.Core.Interfaces;

namespace Metrics.Core
{
    public class MetricProvider : IMetricProvider
    {
        public Task<IEnumerable<IMetric>> CalculateAsync(IExecutionResultStore executionResultStore, CancellationToken cancellationToken)
        {
            var sorted = executionResultStore
                .OrderBy(l => l.Duration)
                .Select(x=>x.Duration.TotalMilliseconds)
                .ToList();

            var quartiles = Quartiles.FromSorted(sorted);
            var moments = Moments.Create(sorted);

            return Task.FromResult<IEnumerable<IMetric>>(new[]
            {
                new SimpleMetric("Average", TimeSpan.FromMilliseconds(moments.Mean)),
                new SimpleMetric("StandardDeviation", TimeSpan.FromMilliseconds(moments.StandardDeviation)),
                new SimpleMetric("Median", TimeSpan.FromMilliseconds(quartiles.Median)),
                new SimpleMetric("Q1", TimeSpan.FromMilliseconds(quartiles.Q1)),
                new SimpleMetric("Q3", TimeSpan.FromMilliseconds(quartiles.Q4))
            });
        }
    }

    public record SimpleMetric(string Name, object Value) : IMetric;
}
