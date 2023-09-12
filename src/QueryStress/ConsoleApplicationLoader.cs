using Autofac;
using QueryStress.App;
using QueryStress.App.Arguments;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace QueryStress;

internal class ConsoleApplicationLoader : ApplicationLoader
{
    private readonly string[] _args;

    private static ApplicationArguments Deserialize(string fileContent)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return deserializer.Deserialize<ApplicationArguments>(fileContent);
    }

    private static ApplicationArguments Merge(string[] args)
    {
        var configFiles = args.Where(x => Path.GetExtension(x) is ".yaml" or ".yml");
        var scriptFiles = args.Single(x => Path.GetExtension(x) is ".sql");

        var applicationArguments = new ApplicationArguments();

        foreach (var configFile in configFiles)
        {
            var arguments = Deserialize(File.ReadAllText(configFile));

            foreach (var (key, value) in arguments)
            {
                applicationArguments.Add(key, value);
            }
        }

        applicationArguments.Add("script",
            new ArgumentsSection
            {
                Type = "file",
                Arguments = new Dictionary<string, string>
                {
                    ["path"] = scriptFiles
                }
            });

        return applicationArguments;
    }

    public ConsoleApplicationLoader(string[] args)
    {
        _args = args;
    }

    public override ContainerBuilder Load(ContainerBuilder builder)
    {
        var appArgs = Merge(_args);

        builder.RegisterInstance(appArgs).AsSelf();

        return builder;
    }
}