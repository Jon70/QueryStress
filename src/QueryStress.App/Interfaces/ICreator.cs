// See https://aka.ms/new-console-template for more information

using QueryStress.App.Arguments;

namespace QueryStress.App.Interfaces;

public interface ICreator<out T>
{
    string TypeName { get; }
    T Create(ArgumentsSection argumentsSection);
}