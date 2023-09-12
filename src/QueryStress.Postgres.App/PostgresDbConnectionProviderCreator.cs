using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Attributes;
using QueryStress.Core.Interfaces;
using QueryStress.Postgres.Core;

[assembly: QueryStressPlugin]

namespace QueryStress.Postgres.App
{
    public class PostgresDbConnectionProviderCreator : ICreator<IConnectionProvider>
    {
        public string TypeName => "postgres";
        public IConnectionProvider Create(ArgumentsSection argumentsSection)
        {
            var connectionString = argumentsSection.ExtractStringArgumentOrThrow("connectionString");

            return new PostgresDbConnectionProvider(connectionString);
        }
    }
}
