using System.Collections.Immutable;
using QueryStress.Core.Interfaces;

namespace QueryStress.Core
{
    public sealed class RequirementService
    {
        private readonly ImmutableArray<IRequirement> _requirements;

        public RequirementService(IEnumerable<IRequirement> requirements)
        {
            _requirements = requirements.ToImmutableArray();
        }

        public T GetRequirement<T>() where T : IRequirement, new()
        {
            return _requirements.OfType<T>().Max() ?? new T();
        }
    }
}
