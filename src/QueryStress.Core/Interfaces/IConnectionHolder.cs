namespace QueryStress.Core.Interfaces;

public interface IConnectionHolder<out T> : IDisposable
{
    T Connection { get; }
}