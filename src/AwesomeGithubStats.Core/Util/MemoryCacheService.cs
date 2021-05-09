using AwesomeGithubStats.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace AwesomeGithubStats.Core.Util
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(
            IMemoryCache memoryCache,
            ILogger<MemoryCacheService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public void Set<T>(string key, T value, TimeSpan? ttl = null)
        {
            _memoryCache.Set(key, value, ttl ?? TimeSpan.FromHours(1));
        }

        public T Get<T>(string key) where T : class
        {
            return _memoryCache.Get<T>(key);
        }

    }
}
