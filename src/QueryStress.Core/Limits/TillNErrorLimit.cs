using QueryStress.Core.Interfaces;

namespace QueryStress.Core.Limits
{
    public class TillNErrorLimit : ILimit, IExecutionHook
    {
        private readonly int _errorLimit;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private int _errorCount;

        public TillNErrorLimit(int errorLimit)
        {
            if (errorLimit <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(errorLimit),
                    $"{nameof(errorLimit)} parameter must be greater than zero. Actual value: {errorLimit}");
            }

            _errorLimit = errorLimit;
            _cancellationTokenSource = new CancellationTokenSource();
            _errorCount = 0;
        }

        public CancellationToken Token => _cancellationTokenSource.Token;

        public Task OnQueryExecutedAsync(ExecutionResult result, CancellationToken cancellationToken)
        {
            if (result.Exception is null)
            {
                return Task.CompletedTask;
            }

            Interlocked.Increment(ref _errorCount);

            if (_errorCount >= _errorLimit)
            {
                _cancellationTokenSource.Cancel();
            }

            return Task.CompletedTask;
        }
    }
}
