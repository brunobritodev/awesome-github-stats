namespace AwesomeGithubStats.Core.Models
{
    public class RankPoints
    {
        public double PullRequests { get; set; }
        public double Commits { get; set; }
        public double PullRequestsToAnotherRepositories { get; set; }
        public double Issues { get; set; }
        public double CreatedRepositories { get; set; }
        public double DirectStars { get; set; }
        public double IndirectStars { get; set; }
        public double ContributedTo { get; set; }
        public double ContributedToOwnRepositories { get; set; }
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