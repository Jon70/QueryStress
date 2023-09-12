namespace QueryStress.Core.Interfaces
{
    public interface IProviderInfo
    {
        string Name { get; }
    }

    public record ProviderInfo(string Name) : IProviderInfo;
}
