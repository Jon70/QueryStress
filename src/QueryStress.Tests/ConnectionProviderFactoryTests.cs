using QueryStress.App.Factories;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Postgres.App;
using QueryStress.Postgres.Core;
using Xunit;

namespace QueryStress.Tests
{
    public class ConnectionProviderFactoryTests
    {
        private readonly SettingsFactory<IConnectionProvider> _factory;

        public ConnectionProviderFactoryTests()
        {
            _factory = new SettingsFactory<IConnectionProvider>("connection", new ICreator<IConnectionProvider>[]
            {
                new PostgresDbConnectionProviderCreator()
            });
        }

        [Fact]
        public void Create_PostgresConnectionProvider_IsCreated()
        {
            var yml = @"
                        connection:
                            type: Postgres
                            arguments: 
                                connectionString: Host=localhost;";

            Assert.IsType<PostgresDbConnectionProvider>(TestUtils.Create(_factory, yml));
        }
    }
}
