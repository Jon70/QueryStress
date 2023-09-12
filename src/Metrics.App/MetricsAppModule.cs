using Autofac;
using Metrics.Core;
using QueryStress.Core.Attributes;
using QueryStress.Core.Interfaces;

[assembly: QueryStressPlugin]

namespace Metrics.App;

public class MetricsAppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MetricProvider>()
            .As<IMetricProvider>();

        builder.RegisterType<LiveMetricProvider>()
            .As<ILiveMetricProvider>()
            .InstancePerDependency();
    }
}