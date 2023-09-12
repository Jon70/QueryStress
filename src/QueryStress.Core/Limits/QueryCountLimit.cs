using QueryStress.Core.Interfaces;

namespace QueryStress.Core.Limits
{
    public class QueryCountLimit : ILimit, IExecutionHook
    {
        private readonly int _count;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private int _current;

        public QueryCountLimit(int count)
        {
            _count = count;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public CancellationToken Token => _cancellationTokenSource.Token;

        public Task OnQueryExecutedAsync(ExecutionResult _, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _current);

            if (_current >= _count)
            {
                _cancellationTokenSource.Cancel();
            }

            return Task.CompletedTask;
        }
    }
}
