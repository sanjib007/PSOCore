using StackExchange.Redis;

namespace Utility.Extentions
{
    public static class RedisExtensions
    {
        public static async Task<bool> TryGetDistributedLockAsync(this IDatabase cache, string key, string lockValue, TimeSpan lockTimeout)
        {
            var lockKey = $"lock:{key}";
            //var lockValue = Guid.NewGuid().ToString();

            var acquired = await cache.StringSetAsync(lockKey, lockValue, lockTimeout, When.NotExists);

            if (!acquired)
            {
                return false;
            }

            var ttl = await cache.KeyTimeToLiveAsync(lockKey);

            if (ttl == null || ttl > lockTimeout)
            {
                ttl = lockTimeout;
                await cache.KeyExpireAsync(lockKey, ttl);
            }

            return true;
        }

        public static async Task ReleaseDistributedLockAsync(this IDatabase cache, string key, string lockValue)
        {
            var lockKey = $"lock:{key}";
            var currentValue = await cache.StringGetAsync(lockKey);

            if (currentValue == lockValue)
            {
                await cache.KeyDeleteAsync(lockKey);
            }
        }
    
    
    }

}
