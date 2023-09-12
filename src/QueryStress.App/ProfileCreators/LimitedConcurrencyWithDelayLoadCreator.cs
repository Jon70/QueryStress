// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.LoadProfiles;

namespace QueryStress.App.ProfileCreators;

public class LimitedConcurrencyWithDelayLoadCreator : ICreator<IProfile>
{
    public string TypeName => "limitedConcurrencyWithDelay";
    public IProfile Create(ArgumentsSection profile)
    {
        return new LimitedConcurrencyWithDelayLoadProfile(profile.ExtractIntArgumentOrThrow("limit"), profile.ExtractTimeSpanArgumentOrThrow("delay"));
    }
}