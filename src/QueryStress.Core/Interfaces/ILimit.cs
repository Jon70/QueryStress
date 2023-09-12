namespace QueryStress.Core.Interfaces
{
    public interface ILimit : ISetting
    {
        public CancellationToken Token { get; }
    }
}
