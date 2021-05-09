using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Services
{
    public class GithubService : IGithubService
    {
        private readonly IGithubUserStore _githubUserStore;

        public GithubService(IGithubUserStore githubUserStore)
        {
            _githubUserStore = githubUserStore;
        }

        public async Task<UserStats> GetUserStats(string username)
        {
            var user = await _githubUserStore.GetUserInformation(username);
            if (user == null)
                return new UserStats();
            var tasks = new List<Task<ContributionsCollection>>();

            foreach (var year in user.ContributionsCollection.ContributionYears)
            {
                tasks.Add(_githubUserStore.GetUserInformationByYear(username, year));
            }

            var result = await Task.WhenAll(tasks).ConfigureAwait(false);
            var userStats = new UserStats(result.Where(w => w != null).ToList(), user);

            return userStats;
        }
    }
}
