// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;
using QueryStress.App.Interfaces;
using QueryStress.Core.Interfaces;

namespace QueryStress.App.Factories;

public class SettingsFactory<T> : ISettingsFactory<T> where T : ISetting
{
    private readonly string _type;
    private readonly Dictionary<string, ICreator<T>> _creators;

    public SettingsFactory(string settingType, IEnumerable<ICreator<T>> creators)
    {
        _type = settingType.ToLowerInvariant();
        _creators = creators.ToDictionary(l => l.TypeName.ToLowerInvariant());
    }

    public T Create(ApplicationArguments arguments)  
    {
        if (!arguments.TryGetValue(_type.ToLowerInvariant(), out var section))
        {
            throw new ApplicationException($"No section with the name of {_type}");
        }

        if (!_creators.TryGetValue(section.Type?.ToLowerInvariant()!, out var settingsCreator))
        {
            throw new ApplicationException($"No setting {typeof(T).Name} with the name of {section.Type}");
        }

        return settingsCreator.Create(section);
    }
}