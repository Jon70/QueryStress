using Autofac;
using QueryStress.Core.Interfaces;

namespace QueryStress.MySql.App;

public class MySqlAppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MySqlDbConnectionProviderCreator>()
            .AsImplementedInterfaces();

        builder.RegisterInstance(new ProviderInfo("MySql"))
            .As<IProviderInfo>();
    }
}