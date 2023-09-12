using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Attributes;
using QueryStress.Core.Interfaces;
using QueryStress.Redis.Core;

[assembly: QueryStressPlugin]

namespace QueryStress.Redis.App
{
    public class RedisConnectionProviderCreator : ICreator<IConnectionProvider>
    {
        public string TypeName => "redis";
        public IConnectionProvider Create(ArgumentsSection argumentsSection)
        {
            var connectionString = argumentsSection.ExtractStringArgumentOrThrow("connectionString");

            return new RedisConnectionProvider(connectionString);
        }
    }
}
