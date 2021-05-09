namespace AwesomeGithubStats.Core.Models.Responses
{
    public class UserData
    {
        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public ContributionsCollection ContributionsCollection { get; set; }
        public PullRequests PullRequests { get; set; }
        public Issues Issues { get; set; }
        public Followers Followers { get; set; }
    }
}