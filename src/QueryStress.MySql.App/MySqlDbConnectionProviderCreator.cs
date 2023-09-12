using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Attributes;
using QueryStress.Core.Interfaces;
using QueryStress.MySql.Core;

[assembly: QueryStressPlugin]

namespace QueryStress.MySql.App
{
    public class MySqlDbConnectionProviderCreator : ICreator<IConnectionProvider>
    {
        public string TypeName => "mySql";
        public IConnectionProvider Create(ArgumentsSection argumentsSection)
        {
            var connectionString = argumentsSection.ExtractStringArgumentOrThrow("connectionString");

            return new MySqlDbConnectionProvider(connectionString);
        }
    }
}
