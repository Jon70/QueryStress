using QueryStress.Core.Interfaces;

namespace Metrics
{
    public class AverageMetricProvider : IMetricProvider
    {
        public Task<IEnumerable<IMetric>> CalculateAsync(IExecutionResultStore executionResultStore, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
