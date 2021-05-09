using System;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, TimeSpan? ttl = null);
        T Get<T>(string key) where T : class;
    }
}