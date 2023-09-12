using QueryStress.Core.Interfaces;
using QueryStress.Core.Requirements;

namespace QueryStress.Core
{
    public abstract class ConnectionProviderBase<T> : IConnectionProvider
    {
        protected readonly string ConnectionString;

        protected ConnectionProviderBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<IExecutable> CreateExecutorAsync(IScriptSource scriptSource, ConnectionRequirement connectionRequirement, CancellationToken cancellationToken)
        {
            var script = await scriptSource.GetScriptAsync(cancellationToken);
            var connections = new T[connectionRequirement.ConnectionCount];

            for (var index = 0; index < connections.Length; index++)
            {
                connections[index] = await CreateOpenConnectionAsync(ConnectionString, cancellationToken);
            }

            return CreateExecutor(script, new ConnectionPool<T>(connections));
        }

        protected abstract Task<T> CreateOpenConnectionAsync(string connectionString, CancellationToken cancellationToken);

        protected abstract IExecutable CreateExecutor(IScript script, IConnectionPool<T> pool);
    }
}
