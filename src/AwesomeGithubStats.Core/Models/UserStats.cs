using AwesomeGithubStats.Core.Models.Responses;
using AwesomeGithubStats.Core.Util;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class UserStats
    {
        public UserStats(List<ContributionsCollection> result, User user)
        {
            var uniqueRepos = result.SelectMany(m => m.CommitContributionsByRepository).Select(s => s.Repository).DistinctBy(a => a.NameWithOwner).ToList();
            var uniquePrs = result.SelectMany(s => s.PullRequestContributionsByRepository).Select(s => s.Repository).DistinctBy(a => a.NameWithOwner).ToList();

            foreach (var repository in uniquePrs)
            {
                if (!uniqueRepos.Any(a => a.NameWithOwner.Equals(repository.NameWithOwner)))
                    uniqueRepos.Add(repository);
            }

            // Remove from uniqueRepo the user repos
            RepositoriesNotOwnedByMe = uniqueRepos.Where(othersRepo => !othersRepo.NameWithOwner.Contains(user.Login)).ToList();
            MyRepositories = uniqueRepos.Where(s => s.NameWithOwner.Contains(user.Login)).ToList();

            Name = user.Name;
            Login = user.Login;

            Commits = result.Sum(s => s.TotalCommitContributions + s.RestrictedContributionsCount);
            PullRequests = user.PullRequests.TotalCount;
            CreatedRepositories = result.Sum(s => s.TotalRepositoryContributions);

            DirectStars = MyRepositories.Sum(s => s.StargazerCount);

            PullRequestsToAnotherRepositories = result.SelectMany(s => s.PullRequestContributionsByRepository).Where(w => !w.Repository.NameWithOwner.Contains(user.Login)).Sum(s => s.Contributions.TotalCount);
            IndirectStars = RepositoriesNotOwnedByMe.Sum(s => s.StargazerCount);
            Issues = user.Issues.TotalCount;

            ContributedTo = MyRepositories.Count() + RepositoriesNotOwnedByMe.Count();
            ContributedToNotOwnerRepositories = RepositoriesNotOwnedByMe.Count();
            ContributedToOwnRepositories = MyRepositories.Count();
            Followers = user.Followers.TotalCount;
        }

        public UserStats()
        {
        }

        /// <summary>
        /// Login of user
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Name of user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Repositories that user has created
        /// </summary>
        public IEnumerable<Repository> MyRepositories { get; set; }
        /// <summary>
        /// Repositories that user contributedFor but wasn't owned by him/her
        /// </summary>
        public IEnumerable<Repository> RepositoriesNotOwnedByMe { get; set; }
        /// <summary>
        /// How many pull requests was made
        /// </summary>
        public int PullRequests { get; set; }
        /// <summary>
        /// How many commits was made
        /// </summary>
        public int Commits { get; set; }
        /// <summary>
        /// How many pr was made for repositories that user wasn't the owner
        /// </summary>
        public int PullRequestsToAnotherRepositories { get; set; }
        /// <summary>
        /// Issues created
        /// </summary>
        public int Issues { get; set; }
        /// <summary>
        /// How many repositories user have created
        /// </summary>
        public int CreatedRepositories { get; set; }
        /// <summary>
        /// Stars from repositories that user has created under his name
        /// </summary>
        public int DirectStars { get; set; }
        /// <summary>
        /// Stars from repositories that user has contributed for
        /// </summary>
        public int IndirectStars { get; set; }
        /// <summary>
        /// How many repositories user has contributed
        /// </summary>
        public int ContributedTo { get; set; }
        /// <summary>
        /// Repositories that user contributed and he/she is the owner
        /// </summary>
        public int ContributedToOwnRepositories { get; set; }
        /// <summary>
        /// Repositories that user contributed and he/she isn't the owner
        /// </summary>
        public int ContributedToNotOwnerRepositories { get; set; }
        /// <summary>
        /// Followers
        /// </summary>
        public int Followers { get; set; }

        public double GetScore(RankPoints rankPoints)
        {
            return (PullRequests * rankPoints.PullRequests) +
                   (Commits * rankPoints.Commits) +
                   (PullRequestsToAnotherRepositories * rankPoints.PullRequestsToAnotherRepositories) +
                   (Issues * rankPoints.Issues) +
                   (CreatedRepositories * rankPoints.CreatedRepositories) +
                   (DirectStars * rankPoints.DirectStars) +
                   (IndirectStars * rankPoints.IndirectStars) +
                   (ContributedTo * rankPoints.ContributedTo) +
                   (ContributedToOwnRepositories * rankPoints.ContributedToOwnRepositories) +
                   (ContributedToNotOwnerRepositories * rankPoints.ContributedToNotOwnerRepositories) +
                   (Followers * rankPoints.Followers);
        }
    }
}