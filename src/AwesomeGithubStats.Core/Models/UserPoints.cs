namespace AwesomeGithubStats.Core.Models
{
    public class RankPoints
    {
        public double PullRequests { get; set; } = 0.2;
        public double Commits { get; set; } = 1;
        public double PullRequestsToAnotherRepositories { get; set; } = 2;
        public double Issues { get; set; } = 1;
        public double CreatedRepositories { get; set; } = 0.5;
        public double DirectStars { get; set; } = 1.0;
        public double IndirectStars { get; set; } = 0.55;
        public double ContributedTo { get; set; } = 0.5;
        public double ContributedToOwnRepositories { get; set; } = 0.2;
        public double ContributedToNotOwnerRepositories { get; set; }
        public double Followers { get; set; }

        public double Total()
        {
            return PullRequests +
                   Commits +
                   PullRequestsToAnotherRepositories +
                   Issues +
                   CreatedRepositories +
                   DirectStars +
                   IndirectStars +
                   ContributedTo +
                   ContributedToOwnRepositories +
                   ContributedToNotOwnerRepositories +
                   Followers;
        }
    }
}