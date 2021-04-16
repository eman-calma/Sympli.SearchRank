using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sympli.SearchRank.Infrastructure.Cache
{
    public class CacheData<T> : ICacheData<T> 
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public async Task<T> GetOrCreate(object key, Func<Task<T>> createItem)
        {
            T cacheEntry;
            if (!_cache.TryGetValue(key, out cacheEntry))
            { 
                cacheEntry = await createItem();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1)//Size amount
                //Priority on removing when reaching size limit (memory pressure)
                .SetPriority(CacheItemPriority.High)
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromHours(1))
                // Remove from cache after this time, regardless of sliding expiration
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(key, cacheEntry);
            }
            return cacheEntry;
        }
    }
}
