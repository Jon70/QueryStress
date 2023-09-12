using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core;
using QueryStress.Core.Interfaces;
using QueryStress.Core.Requirements;

namespace QueryStress.App
{
    public class ScenarioBuilder : IScenarioBuilder
    {
        private readonly ISettingsFactory<IProfile> _profilesFactory;
        private readonly ISettingsFactory<ILimit> _limitFactory;
        private readonly ISettingsFactory<IConnectionProvider> _connectionFactory;
        private readonly ISettingsFactory<IScriptSource> _scriptSourceFactory;

        public ScenarioBuilder(ISettingsFactory<IProfile> profilesFactory, ISettingsFactory<ILimit> limitFactory,
            ISettingsFactory<IConnectionProvider> connectionFactory, ISettingsFactory<IScriptSource> scriptSourceFactory)
        {
            _profilesFactory = profilesFactory;
            _limitFactory = limitFactory;
            _connectionFactory = connectionFactory;
            _scriptSourceFactory = scriptSourceFactory;
        }

        public async Task<QueryExecutor> BuildAsync(ApplicationArguments arguments, IExecutionResultStore executionResult, IEnumerable<IExecutionHook> liveMetrics,  CancellationToken cancellationToken)
        {
            var profile = _profilesFactory.Create(arguments);
            var limit = _limitFactory.Create(arguments);
            var connection = _connectionFactory.Create(arguments);
            var scriptSource = _scriptSourceFactory.Create(arguments);

            var requirements = profile.Requirements
                .Concat(limit.Requirements)
                .Concat(connection.Requirements)
                .Concat(scriptSource.Requirements);

            var requirementsService = new RequirementService(requirements);

            var executor = await connection.CreateExecutorAsync(scriptSource, requirementsService.GetRequirement<ConnectionRequirement>(), cancellationToken);

            return new QueryExecutor(executor, profile, limit, executionResult, liveMetrics);
        }
    }
}
