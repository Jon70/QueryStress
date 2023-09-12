// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.LoadProfiles;

namespace QueryStress.App.ProfileCreators;

public class SequentialWithDelayLoadCreator : ICreator<IProfile>
{
    public string TypeName => "sequentialWithDelay";
    public IProfile Create(ArgumentsSection profile)
    {
        return new SequentialWithDelayLoadProfile(profile.ExtractTimeSpanArgumentOrThrow("delay"));
    }
}