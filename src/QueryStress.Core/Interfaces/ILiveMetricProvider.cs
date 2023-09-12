namespace QueryStress.Core.Interfaces
{
    public interface ILiveMetricProvider : IExecutionHook
    {
        IEnumerable<IMetric> GetMetrics();
    }
}
