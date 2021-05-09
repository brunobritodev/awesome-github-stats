using System.Collections.Generic;
using System.Diagnostics;

namespace AwesomeGithubStats.Core.Models.Responses
{
    [DebuggerDisplay("{Year}")]
    public class ContributionsCollection
    {
        public int Year { get; set; }
        public int[] ContributionYears { get; set; }
        public int TotalCommitContributions { get; set; }
        public int TotalRepositoryContributions { get; set; }
        public int RestrictedContributionsCount { get; set; }
        public IEnumerable<RepositoryContribution> PullRequestContributionsByRepository { get; set; }
        public IEnumerable<RepositoryContribution> CommitContributionsByRepository { get; set; }
    }
}