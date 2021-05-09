using System.Diagnostics;

namespace AwesomeGithubStats.Core.Models.Responses
{
    [DebuggerDisplay("{Repository.NameWithOwner}")]
    public class CommitcontributionsByRepository
    {
        public Repository Repository { get; set; }
        public Contributions Contributions { get; set; }
    }
}