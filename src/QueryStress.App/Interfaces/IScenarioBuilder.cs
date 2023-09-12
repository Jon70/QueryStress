using QueryStress.App.Arguments;
using QueryStress.Core;
using QueryStress.Core.Interfaces;

namespace QueryStress.App.Interfaces
{
    public interface IScenarioBuilder
    {
        Task<QueryExecutor> BuildAsync(ApplicationArguments arguments, IExecutionResultStore executionResult, IEnumerable<IExecutionHook> liveMetrics, CancellationToken cancellationToken);
    }
}
