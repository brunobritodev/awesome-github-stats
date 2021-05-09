using System.Diagnostics;

namespace AwesomeGithubStats.Core.Models.Responses
{
    [DebuggerDisplay("{Repository.NameWithOwner}")]
    public class PullRequestContributionsByRepository
    {
        public Contributions Contributions { get; set; }
        public Repository Repository { get; set; }
    }
}