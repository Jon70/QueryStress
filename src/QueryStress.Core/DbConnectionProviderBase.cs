using System.Data;
using QueryStress.Core.Interfaces;

namespace QueryStress.Core
{
    public abstract class DbConnectionProviderBase<T> : ConnectionProviderBase<T> where T : IDbConnection
    {
        protected DbConnectionProviderBase(string connectionString) : base(connectionString)
        { }

        protected override IExecutable CreateExecutor(IScript script, IConnectionPool<T> pool)
            => new DbExecutableBase<T>(script, pool);
    }
}
