using AwesomeGithubStats.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AwesomeGithubStats.Core.Util
{
    public class MemoryCacheService : ICacheService
    {
        public static MemoryCacheEntryOptions DefaultOptions => new() { SlidingExpiration = TimeSpan.FromHours(1) };
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(
            IMemoryCache memoryCache,
            ILogger<MemoryCacheService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public void Set<T>(string key, T value, DateTimeOffset? ttl = null)
        {
            _memoryCache.Set(key, value, DefaultOptions);
        }

        public T Get<T>(string key) where T : class
        {
            return _memoryCache.Get<T>(key);
        }

    }
}
