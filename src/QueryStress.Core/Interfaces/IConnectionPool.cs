namespace QueryStress.Core.Interfaces
{
    public interface IConnectionPool<out T> : IDisposable
    {
        IConnectionHolder<T> UseConnection();
    }
}
