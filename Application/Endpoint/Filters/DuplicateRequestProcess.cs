using Microsoft.AspNetCore.Mvc.Filters;
using DLL.Repositories.Cache;
using Microsoft.Extensions.Configuration;
using Utility.ConstantKeys;
using Utility.Extentions;
using Utility.Exceptions;

namespace Endpoint.Filters
{
    public class DuplicateRequestProcess : IAsyncActionFilter
    {

        private readonly ICacheRepository _cache;
        private readonly IConfiguration _configuration;
        private readonly TimeSpan _lockTimeout;

        //int _expireMinute;
        public DuplicateRequestProcess(ICacheRepository cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
            _lockTimeout = TimeSpan.FromMinutes(1);  // one minutes
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                         ActionExecutionDelegate next)
        {
            var body = context.HttpContext.Request.BodyToString();
            var cache_key = $"{SystemCachKeys.InProgress}{body}";
            var cache_value = "ProcessingRequest";

            var result = await _cache.TryGetDistributedLockAsync(cache_key, cache_value, _lockTimeout);
            if (!result)
            {
                throw new BaseException("Previous Request processing. Please wait some times.");
            }

            try
            {
                await next(); // the actual action
            }
            finally
            {
                await _cache.ReleaseDistributedLockAsync(cache_key, cache_value);
            }


        }
    }

}
