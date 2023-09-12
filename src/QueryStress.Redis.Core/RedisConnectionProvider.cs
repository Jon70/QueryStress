using QueryStress.Core;
using QueryStress.Core.Interfaces;
using StackExchange.Redis;

namespace QueryStress.Redis.Core
{
    public class RedisConnectionProvider : ConnectionProviderBase<ConnectionMultiplexer>
    {
        public RedisConnectionProvider(string connectionString) : base(connectionString)
        {
        }

        protected override async Task<ConnectionMultiplexer> CreateOpenConnectionAsync(string connectionString,
            CancellationToken cancellationToken)
        {
            var connection = await ConnectionMultiplexer.ConnectAsync(ConnectionString);

            return connection;
        }

        protected override IExecutable CreateExecutor(IScript script, IConnectionPool<ConnectionMultiplexer> pool)
        {
            return new RedisExecutor(script, pool);
        }
    }
}
