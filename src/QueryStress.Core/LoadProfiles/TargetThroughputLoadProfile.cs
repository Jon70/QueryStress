using QueryStress.Core.Interfaces;

namespace QueryStress.Core.LoadProfiles
{
    public class TargetThroughputLoadProfile : IProfile
    {
        private readonly TimeSpan _delay;
        private DateTime? _nextExecution;

        public TargetThroughputLoadProfile(int targetRps)
        {
            _delay = TimeSpan.FromMilliseconds(1000f / targetRps);
        }

        public async Task WhenNextCanBeExecutedAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            if (_nextExecution == null || now > _nextExecution)
            {
                _nextExecution = now + _delay;
            }
            else
            {
                var delta = _nextExecution.Value - now;

                _nextExecution += _delay;
                await Task.Delay(delta, cancellationToken);
            }
        }
    }
}
