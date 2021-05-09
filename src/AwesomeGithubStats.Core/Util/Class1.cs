using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Util
{
    public class MemoryCacheService : ICacheService
    {
        public static MemoryCacheEntryOptions DefaultOptions => new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromHours(1) };
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;
        private readonly ILogger<MemoryCacheService> _logger;

        public MemoryCacheService(
            IMemoryCache memoryCache,
            IConfiguration config,
            ILogger<MemoryCacheService> logger)
        {
            _memoryCache = memoryCache;
            _config = config;
            _logger = logger;
        }

        public string ObterCachePorChave(string chave, DateTimeOffset? ttl = null)
        {
            var item = GetAsync<string>(chave).Result;
            if (item.IsPresent())
                return item;

            var valorDb = _cacheContext.DadosCache.FirstOrDefault(w => w.Key == chave)?.Value;

            if (valorDb.IsPresent())
                SetAsync(chave, valorDb, ttl).Wait();

            return valorDb;

        }

        public Task SetAsync<T>(string key, T value, DateTimeOffset? ttl = null)
        {
            _memoryCache.Set(key, value, MemoryCacheService.DefaultOptions);
            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            return Task.FromResult(_memoryCache.Get<T>(key));
        }

    }
}
