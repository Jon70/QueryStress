using QueryStress.App.Arguments;

namespace QueryStress.App.Interfaces
{
    public interface ISettingsFactory<out T>
    {
        T Create(ApplicationArguments arguments);
    }
}
