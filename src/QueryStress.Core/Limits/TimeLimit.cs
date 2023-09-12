using QueryStress.Core.Interfaces;

namespace QueryStress.Core.Limits
{
    public class TimeLimit : ILimit
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public TimeLimit(TimeSpan time)
        {
            _cancellationTokenSource = new CancellationTokenSource(time);
        }

        public CancellationToken Token => _cancellationTokenSource.Token;
    }
}
