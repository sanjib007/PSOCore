using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace DLL.Repositories.Cache
{
    public interface ICacheRepository
    {
        Task<T> GetCacheAsync<T>(string key);
        Task ReleaseDistributedLockAsync(string key, string lockValue);
        Task RemoveCacheAsync(string key);
        Task SetCacheAsync(string cacheKey, object value, int cacheExpiryMin = 0);
        Task<bool> TryGetDistributedLockAsync(string key, string lockValue, TimeSpan lockTimeout);
    }


    public class RedisCacheRepository : ICacheRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabase _cache;
        DistributedCacheEntryOptions cacheExpiryOptions;
        int _expiryMin;
        public RedisCacheRepository(IConfiguration configuration, IDistributedCache distributedCache, IDatabase cache)
        {

            _configuration = configuration;
            _distributedCache = distributedCache;
            _cache = cache;
            _expiryMin = Convert.ToInt32(_configuration.GetSection("Redis:ExpiryMin").Value);
            cacheExpiryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(_expiryMin));
        }

        public async Task SetCacheAsync(string cacheKey, object value, int cacheExpiryMin = 0)
        {
            if (cacheExpiryMin > 0)
            {
                cacheExpiryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(cacheExpiryMin));
            }

            var serializedDate = JsonConvert.SerializeObject(value);
            var redisData = Encoding.UTF8.GetBytes(serializedDate);
            await _distributedCache.SetAsync(cacheKey, redisData, cacheExpiryOptions);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var redisData = await _distributedCache.GetAsync(key);
            if (redisData != null)
            {
                var serializedData = Encoding.UTF8.GetString(redisData);
                var result = JsonConvert.DeserializeObject<T>(serializedData);
                return result;
            }
            return default(T);
        }
        public async Task RemoveCacheAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);

        }



        #region Lock
        public async Task<bool> TryGetDistributedLockAsync(string key, string lockValue, TimeSpan lockTimeout)
        {
            var lockKey = $"lock:{key}";
            var acquired = await _cache.LockTakeAsync(lockKey, lockValue, lockTimeout);
            if (!acquired)
            {
                return false;
            }
            return true;
        }
        public async Task ReleaseDistributedLockAsync(string key, string lockValue)
        {
            var lockKey = $"lock:{key}";
            await _cache.LockReleaseAsync(lockKey, lockValue);
        }
        #endregion

    }

}
