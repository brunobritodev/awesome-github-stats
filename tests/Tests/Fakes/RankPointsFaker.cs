using AwesomeGithubStats.Core.Models;
using Bogus;

namespace Tests.Fakes
{
    class RankPointsFaker
    {
        public static Faker<RankPoints> Get()
        {
            return new Faker<RankPoints>()
                .RuleFor(r => r.PullRequests, f => f.Random.Double())
                .RuleFor(r => r.Commits, f => f.Random.Double())
                .RuleFor(r => r.PullRequestsToAnotherRepositories, f => f.Random.Double())
                .RuleFor(r => r.Issues, f => f.Random.Double())
                .RuleFor(r => r.CreatedRepositories, f => f.Random.Double())
                .RuleFor(r => r.DirectStars, f => f.Random.Double())
                .RuleFor(r => r.IndirectStars, f => f.Random.Double())
                .RuleFor(r => r.ContributedTo, f => f.Random.Double())
                .RuleFor(r => r.ContributedToOwnRepositories, f => f.Random.Double())
                .RuleFor(r => r.ContributedToNotOwnerRepositories, f => f.Random.Double())
                .RuleFor(r => r.Followers, f => f.Random.Double());

        }
    }
}
