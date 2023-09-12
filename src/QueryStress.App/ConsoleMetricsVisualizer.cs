using System.Text;
using QueryStress.Core.Interfaces;

namespace QueryStress.App
{
    public class ConsoleMetricsVisualizer : IMetricsVisualizer
    {
        public Task<IVisualization> VisualizeAsync(IEnumerable<IMetric> metrics, CancellationToken cancellationToken)
        {
            var sb = new StringBuilder();

            foreach (var metric in metrics)
            {
                sb.AppendLine($"{metric.Name}: {metric.Value} ms");
            }

            return Task.FromResult<IVisualization>(new ConsoleVisualization(sb.ToString()));
        }

        private class ConsoleVisualization : IVisualization
        {
            private readonly string _view;

            public ConsoleVisualization(string view)
            {
                _view = view;
            }

            public override string ToString()
            {
                return _view;
            }
        }
    }
}
