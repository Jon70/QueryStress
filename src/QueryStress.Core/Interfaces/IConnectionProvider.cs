using QueryStress.Core.Requirements;

namespace QueryStress.Core.Interfaces
{
    public interface IConnectionProvider : ISetting
    {
        Task<IExecutable> CreateExecutorAsync(IScriptSource scriptSource, ConnectionRequirement connection, CancellationToken cancellationToken);
    }
}
