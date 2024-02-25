namespace ProjectX.Application.Contracts
{
    public interface ICacheService
    {
        T TryGet<T>(string cacheKey);
        bool Set<T>(string cacheKey, T value, DateTimeOffset expirationTime);
        object Remove(string cacheKey);
    }
}
