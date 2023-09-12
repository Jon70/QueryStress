namespace QueryStress.Core.Interfaces
{
    public interface IExecutionResultStore : IExecutionHook, IEnumerable<ExecutionResult>
    {
        //public ExecutionResult CalcMetricAsync(CancellationToken cancellationToken);
    }
}
