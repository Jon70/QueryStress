using System.Data;
using QueryStress.Core.Interfaces;
using QueryStress.Core.ScriptSource;
using System.Data.Common;

namespace QueryStress.Core
{
    public class DbExecutableBase<T> : IExecutable where T : IDbConnection
    {
        private readonly IConnectionPool<T> _connection;
        private readonly TextScript _textScript;

        public DbExecutableBase(IScript script, IConnectionPool<T> connection)
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
            await using var cmd = (DbCommand)holder.Connection.CreateCommand();
            cmd.CommandText = _textScript.Text;

            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
            await reader.ReadAsync(cancellationToken);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
