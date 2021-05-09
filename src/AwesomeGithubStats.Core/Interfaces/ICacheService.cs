using System;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, DateTimeOffset? ttl = null);
        T Get<T>(string key) where T : class;
    }
}