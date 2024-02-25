using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectX.Application.Contracts;
using System.Runtime.Caching;

namespace ProjectX.Persistence.Repositories
{
    public class CacheService : ICacheService
    {
        ObjectCache _memoryCache = MemoryCache.Default;

        public T TryGet<T>(string cacheKey)
        {
            try
            {
                T item = (T)_memoryCache.Get(cacheKey);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Set<T>(string cacheKey, T value, DateTimeOffset expirationTime)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    _memoryCache.Set(cacheKey, value, expirationTime);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }
        public object Remove(string cacheKey)
        {
            try
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    return _memoryCache.Remove(cacheKey);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
