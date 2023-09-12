using System.Collections;
using QueryStress.Core.Interfaces;

namespace QueryStress.Core
{
    public class ExecutionResultStore : IExecutionResultStore
    {
        private readonly LinkedList<ExecutionResult> _executionResults = new();

        public Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken)
        {
            _executionResults.AddLast(result);
            return Task.CompletedTask;
        }

        public IEnumerator<ExecutionResult> GetEnumerator()
        {
            return _executionResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
