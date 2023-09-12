namespace QueryStress.Core;

public record ExecutionResult(DateTime QueryStartTime, DateTime QueryEndTime, TimeSpan Duration, Exception? Exception)
{
    public static ExecutionResult Empty { get; } = new(default, default, default, default);
}