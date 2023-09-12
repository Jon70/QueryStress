using QueryStress.App.Arguments;
using QueryStress.App.Factories;
using QueryStress.Core.Interfaces;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace QueryStress.Tests
{
    public static class TestUtils
    {
        public static T Create<T>(SettingsFactory<T> factory, string yml) where T : ISetting
        {
            var args = Deserialize(yml);
            return factory.Create(args);
        }

        public static ApplicationArguments Deserialize(string fileContent)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<ApplicationArguments>(fileContent);
        }
    }
}
