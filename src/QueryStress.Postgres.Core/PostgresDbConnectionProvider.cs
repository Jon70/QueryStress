using Npgsql;
using QueryStress.Core;
using QueryStress.Core.Interfaces;

namespace QueryStress.Postgres.Core
{
    public class PostgresDbConnectionProvider : DbConnectionProviderBase<NpgsqlConnection>
    {
        public PostgresDbConnectionProvider(string connectionString) : base(connectionString)
        {
        }

        protected override async Task<NpgsqlConnection> CreateOpenConnectionAsync(string connectionString,
            CancellationToken cancellationToken)
        {
            var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);

            return connection;
        }

        protected override IExecutable CreateExecutor(IScript script, IConnectionPool<NpgsqlConnection> pool)
        {
            return new DbExecutableBase<NpgsqlConnection>(script, pool);
        }
    }
}
