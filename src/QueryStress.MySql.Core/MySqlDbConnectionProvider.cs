using MySqlConnector;
using QueryStress.Core;
using QueryStress.Core.Interfaces;

namespace QueryStress.MySql.Core
{
    public class MySqlDbConnectionProvider : DbConnectionProviderBase<MySqlConnection>
    {
        public MySqlDbConnectionProvider(string connectionString) : base(connectionString)
        {
        }

        protected override async Task<MySqlConnection> CreateOpenConnectionAsync(string connectionString, CancellationToken cancellationToken)
        {
            var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }

        protected override IExecutable CreateExecutor(IScript script, IConnectionPool<MySqlConnection> pool)
        {
            return new DbExecutableBase<MySqlConnection>(script, pool);
        }
    }
}
