using QueryStress.Core.Interfaces;
using QueryStress.Core.ScriptSource;
using StackExchange.Redis;

namespace QueryStress.Redis.Core
{
    internal class RedisExecutor : IExecutable
    {
        private readonly IConnectionPool<ConnectionMultiplexer> _connection;
        private readonly TextScript _textScript;

        public RedisExecutor(IScript script, IConnectionPool<ConnectionMultiplexer> connection)
        {
            if (script is not TextScript textScript)
            {
                throw new ApplicationException("The only supported script type is TextScript");
            }

            _textScript = textScript;
            _connection = connection;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var holder = _connection.UseConnection();
            var db = holder.Connection.GetDatabase();
            _ = await db.ScriptEvaluateAsync(_textScript.Text);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
