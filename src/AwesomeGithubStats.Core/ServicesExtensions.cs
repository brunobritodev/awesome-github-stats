using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Services;
using AwesomeGithubStats.Core.Store;
using AwesomeGithubStats.Core.Util;
using Microsoft.Extensions.Configuration;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Configure Notification
    /// </summary>
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureGithubServices(this IServiceCollection services, IConfiguration configuration, string githubUrl = "https://api.github.com")
        {

            var pats = configuration.GetSection("PATS").AsEnumerable();
            foreach (var pat in pats)
            {
                if (pat.Value != null)
                    lock (GithubOptions.PersonalAccessTokenUsage)
                    {
                        if (!GithubOptions.PersonalAccessTokenUsage.ContainsKey(pat.Value))
                            GithubOptions.PersonalAccessTokenUsage.Add(pat.Value, 0);
                    }
            }

            services.Configure<RankPoints>(options => configuration.GetSection("RankPoints").Bind(options));
            services.Configure<RankDegree>(options => configuration.GetSection("RankDegree").Bind(options));
            services.AddMemoryCache();
            services.AddScoped<IGithubService, GithubService>();
            services.AddScoped<IGithubUserStore, GithubUserStore>();
            services.AddScoped<IRankService, RankService>();
            services.AddScoped<ISvgService, SvgService>();
            services.AddScoped<ICacheService, MemoryCacheService>();

            services.AddHttpClient("github", c =>
            {
                var pat = GithubOptions.NextPat();
                c.BaseAddress = new Uri(githubUrl);
                // Github requires a user-agent
                c.DefaultRequestHeaders.Add("Authorization", $"bearer {pat}");

                // Github requires a user-agent
                c.DefaultRequestHeaders.Add("User-Agent", "Awesome-Github-Stats");
            });

            return services;
        }
    }
}