namespace QueryStress.Core.Interfaces
{
    public interface IExecutionHook
    {
        Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken);
    }
}
