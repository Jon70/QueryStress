// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.Limits;

namespace QueryStress.App.LimitCreator;

public class TillNErrorCreator : ICreator<ILimit>
{
    public string TypeName => "tillNError";
    public ILimit Create(ArgumentsSection profile) => new QueryCountLimit(profile.ExtractIntArgumentOrThrow("errorLimit"));
}