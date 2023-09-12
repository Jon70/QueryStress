using Autofac;
using QueryStress.Core.Interfaces;

namespace QueryStress.Redis.App;

public class RedisAppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RedisConnectionProviderCreator>()
            .AsImplementedInterfaces();

        builder.RegisterInstance(new ProviderInfo("Redis"))
            .As<IProviderInfo>();
    }
}