﻿// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;
using QueryStress.Core.LoadProfiles;

namespace QueryStress.App.ProfileCreators;

public class LimitedConcurrencyLoadCreator : ICreator<IProfile>
{
    public string TypeName => "limitedConcurrency";
    public IProfile Create(ArgumentsSection profile)
    {
        return new LimitedConcurrencyLoadProfile(profile.ExtractIntArgumentOrThrow("limit"));
    }
}