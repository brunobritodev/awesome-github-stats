namespace AwesomeGithubStats.Core.Models
{
    public class RankPoints
    {
        public double Commits { get; set; } = 1;
        public double ContributedToNotOwnerRepositories { get; set; } = 10;
        public double ContributedToOwnRepositories { get; set; } = 1;
        public double CreatedRepositories { get; set; } = 1;
        public double DirectStars { get; set; } = 3.5;
        public double Followers { get; set; } = 1;
        public double IndirectStars { get; set; } = 1;
        public double Issues { get; set; } = 1;
        public double PullRequests { get; set; } = 1;
        public double PullRequestsToAnotherRepositories { get; set; } = 5;
        public double CommitsToMyRepositories { get; set; } = 1;
        public double CommitsToAnotherRepositories { get; set; } = 10;
        public double ContributedTo { get; set; } = 1;

        public double Total(string login)
        {
            if (!string.IsNullOrEmpty(login) && login.Equals("sindresorhus"))
            {
                // Average weighted
                return PullRequests +
                     CommitsToMyRepositories +
                     PullRequestsToAnotherRepositories +
                     CommitsToAnotherRepositories +
                     Commits +
                     Issues +
                     CreatedRepositories +
                     DirectStars +
                     IndirectStars +
                     ContributedTo +
                     ContributedToOwnRepositories +
                     ContributedToNotOwnerRepositories +
                     Followers;

            }

            return 1;
        }
    }
}