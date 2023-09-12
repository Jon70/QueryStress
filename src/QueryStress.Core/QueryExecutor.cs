using System.Collections.Immutable;
using System.Diagnostics;
using QueryStress.Core.Interfaces;

namespace QueryStress.Core;

public class QueryExecutor
{
    private readonly IExecutable _executable;
    private readonly IProfile _loadProfile;
    private readonly ILimit _limit;
    private readonly ImmutableArray<IExecutionHook> _executionHooks;

    public QueryExecutor(IExecutable executable, IProfile loadProfile, ILimit limit,
        IExecutionResultStore executionResultStore, IEnumerable<IExecutionHook> liveMetrics)
    {
        _executable = executable;
        _loadProfile = loadProfile;
        _limit = limit;

        var hooks = ImmutableArray.CreateBuilder<IExecutionHook>();

        if (loadProfile is IExecutionHook profileHook)
        {
            hooks.Add(profileHook);
        }

        if (limit is IExecutionHook limitHook)
        {
            hooks.Add(limitHook);
        }

        if (executionResultStore is IExecutionHook storeHook)
        {
            hooks.Add(storeHook);
        }

        hooks.AddRange(liveMetrics);
        _executionHooks = hooks.ToImmutableArray();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _limit.Token).Token;

        try
        {
            while (!token.IsCancellationRequested)
            {
                await _loadProfile.WhenNextCanBeExecutedAsync(token);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                var queryStartTime = DateTime.Now;
                var sw = Stopwatch.StartNew();
                var _ = await _executable.ExecuteAsync(token).ContinueWith(async executionTask =>
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    sw.Stop();

                    var queryEndTime = DateTime.Now;
                    var result = new ExecutionResult(queryStartTime, queryEndTime, sw.Elapsed, executionTask.Exception);

                    await Task.WhenAll(_executionHooks.Select(x => x.OnQueryExecutedAsync(result, token)));
                }, token);
            }
        }
        catch (OperationCanceledException) { }
    }
}