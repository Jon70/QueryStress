using QueryStress.App.Factories;
using QueryStress.App.Interfaces;
using QueryStress.App.LimitCreator;
using QueryStress.Core.Interfaces;
using QueryStress.Core.Limits;
using Xunit;

namespace QueryStress.Tests
{
    public class LimitFactoryTests
    {
        private readonly SettingsFactory<ILimit> _factory;

        public LimitFactoryTests()
        {
            _factory = new SettingsFactory<ILimit>("limit", new ICreator<ILimit>[]
            {
                new QueryCountCreator(),
                new TimeCreator()
            });
        }

        [Fact]
        public void Create_QueryCountLimit_IsCreated()
        {
            var yml = @"
                        limit:
                            type: QueryCount
                            arguments: 
                                limit: 2";

            Assert.IsType<QueryCountLimit>(TestUtils.Create(_factory, yml));
        }

        [Fact]
        public void Create_TimeLimit_IsCreated()
        {
            var yml = @"
                        limit:
                            type: time
                            arguments: 
                                limit: 4";

            Assert.IsType<TimeLimit>(TestUtils.Create(_factory, yml));
        }
    }
}
