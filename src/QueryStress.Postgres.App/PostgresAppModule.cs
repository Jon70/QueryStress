using Autofac;
using QueryStress.Core.Interfaces;

namespace QueryStress.Postgres.App;

public class PostgresAppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PostgresDbConnectionProviderCreator>()
            .AsImplementedInterfaces();

        builder.RegisterInstance(new ProviderInfo("Postgres"))
            .As<IProviderInfo>();
    }
}