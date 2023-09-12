// See https://aka.ms/new-console-template for more information

namespace QueryStress.App.Arguments;

public class ArgumentsSection
{
    public string? Type { get; set; }
    public Dictionary<string, string>? Arguments { get; set; }
}