// See https://aka.ms/new-console-template for more information

using Autofac;
using QueryStress;
using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;

var loader = new ConsoleApplicationLoader(args);
var container = loader.Load(new ContainerBuilder()).Build();

var builder = container.Resolve<IScenarioBuilder>();
var appArgs = container.Resolve<ApplicationArguments>();
var executionResultStore = container.Resolve<IExecutionResultStore>();
var liveMetrics = container.Resolve<ILiveMetricProvider[]>();
var visualizer = container.ResolveKeyed<IMetricsVisualizer>("Console");

var executor = await builder.BuildAsync(appArgs, executionResultStore, liveMetrics, default);

var ex =  executor.ExecuteAsync(CancellationToken.None);

while (!ex.IsCompleted)
{
    await Task.WhenAny(ex, Task.Delay(200));
    var visualization = await visualizer.VisualizeAsync(liveMetrics.SelectMany(x=>x.GetMetrics()), default);

    Console.Clear();
    Console.WriteLine(visualization);
}

var calculator = container.Resolve<IMetricsCalculator>();
var metrics = await calculator.CalculateAsync(executionResultStore, default);
var visualization1 = await visualizer.VisualizeAsync(metrics, default);

Console.WriteLine(visualization1);

Console.ReadLine();