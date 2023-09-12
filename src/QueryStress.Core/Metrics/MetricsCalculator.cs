using System.Collections.Immutable;
using QueryStress.Core.Interfaces;

namespace QueryStress.Core.Metrics
{
    public class MetricsCalculator : IMetricsCalculator
    {
        private readonly IEnumerable<IMetricProvider> _metricProviders;

        public MetricsCalculator(IEnumerable<IMetricProvider> metricProviders)
        {
            _metricProviders = metricProviders;
        }

        public async Task<IEnumerable<IMetric>> CalculateAsync(IExecutionResultStore executionResultStore, CancellationToken cancellationToken)
        {
            var metrics = ImmutableArray.CreateBuilder<IMetric>();

            foreach (var metricProvider in _metricProviders)
            {
                var metric = await metricProvider.CalculateAsync(executionResultStore, cancellationToken);

                metrics.AddRange(metric);
            }

            return metrics;
        }
    }
}
