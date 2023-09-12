using Autofac;
using QueryStress.App.Factories;
using QueryStress.App.Interfaces;
using QueryStress.Core;
using QueryStress.Core.Interfaces;
using QueryStress.Core.Metrics;

namespace QueryStress.App
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Creator"))
                .AsImplementedInterfaces();

            builder.RegisterType<SettingsFactory<IProfile>>()
                .As<ISettingsFactory<IProfile>>()
                .WithParameter("settingType", "profile");

            builder.RegisterType<SettingsFactory<ILimit>>()
                .As<ISettingsFactory<ILimit>>()
                .WithParameter("settingType", "limit");

            builder.RegisterType<SettingsFactory<IConnectionProvider>>()
                .As<ISettingsFactory<IConnectionProvider>>()
                .WithParameter("settingType", "connection");

            builder.RegisterType<SettingsFactory<IScriptSource>>()
                .As<ISettingsFactory<IScriptSource>>()
                .WithParameter("settingType", "script");

            builder.RegisterType<ScenarioBuilder>()
                .As<IScenarioBuilder>();

            builder.RegisterType<ExecutionResultStore>()
                .As<IExecutionResultStore>();

            builder.RegisterType<MetricsCalculator>()
                .As<IMetricsCalculator>();

            builder.RegisterType<ConsoleMetricsVisualizer>()
                .Keyed<IMetricsVisualizer>("Console");
        }
    }
}
