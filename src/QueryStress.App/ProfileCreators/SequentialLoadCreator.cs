// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.LoadProfiles;

namespace QueryStress.App.ProfileCreators;

public class SequentialLoadCreator : ICreator<IProfile>
{
    public string TypeName => "sequential";
    public IProfile Create(ArgumentsSection profile) => new SequentialLoadProfile();
}