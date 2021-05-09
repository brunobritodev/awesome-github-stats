using System.Diagnostics;

namespace AwesomeGithubStats.Core.Models.Responses
{
    [DebuggerDisplay("{Repository.NameWithOwner}")]
    public class RepositoryContribution
    {
        public Contributions Contributions { get; set; }
        public Repository Repository { get; set; }
    }
}