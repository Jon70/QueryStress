namespace QueryStress.Core.Interfaces
{
    public interface IMetricProvider
    {
        Task<IEnumerable<IMetric>> CalculateAsync(IExecutionResultStore executionResultStore, CancellationToken cancellationToken);
    }
}
