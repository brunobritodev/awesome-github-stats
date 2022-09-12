using AwesomeGithubStats.Core.Models.Responses;
using AwesomeGithubStats.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class UserStats
    {
        public UserStats(List<ContributionsCollection> result, User user)
        {
            var uniqueRepos = result.SelectMany(m => m.CommitContributionsByRepository).DistinctByProperty(a => a.Repository.NameWithOwner).ToList();
            var uniquePrs = result.SelectMany(s => s.PullRequestContributionsByRepository).DistinctByProperty(a => a.Repository.NameWithOwner).ToList();

            foreach (var repository in uniquePrs)
            {
                if (!uniqueRepos.Any(a => a.Repository.NameWithOwner.Equals(repository.Repository.NameWithOwner)))
                    uniqueRepos.Add(repository);
            }

            Name = user.Name;
            Login = user.Login;

            // Remove from uniqueRepo the user repos
            var myContributionsToAnotherRepositories = uniqueRepos.Where(othersRepo => !othersRepo.Repository.NameWithOwner.Contains(user.Login)).ToList();
            var myContributions = uniqueRepos.Where(s => s.Repository.NameWithOwner.Contains(user.Login)).ToList();


            Commits = result.Sum(s => s.TotalCommitContributions + s.RestrictedContributionsCount);

            PullRequests = user.PullRequests.TotalCount;
            CreatedRepositories = result.Sum(s => s.TotalRepositoryContributions);
            CommitsToMyRepositories = myContributions.Sum(s => s.Contributions.TotalCount);
            DirectStars = myContributions.Sum(s => s.Repository.StargazerCount);


            PullRequestsToAnotherRepositories = result.SelectMany(s => s.PullRequestContributionsByRepository).Where(w => !w.Repository.NameWithOwner.Contains(user.Login)).Sum(s => s.Contributions.TotalCount);
            IndirectStars = myContributionsToAnotherRepositories.Sum(s => s.Repository.StargazerCount);
            Issues = user.Issues.TotalCount;
            CommitsToAnotherRepositories = Commits - CommitsToMyRepositories;

            ContributedTo = myContributions.Count() + myContributionsToAnotherRepositories.Count();
            ContributedToNotOwnerRepositories = myContributionsToAnotherRepositories.Count();
            ContributedToOwnRepositories = myContributions.Count();
            Followers = user.Followers.TotalCount;
            LastFetch = DateTime.Now;
        }


        public int CommitsToMyRepositories { get; set; }


        public UserStats()
        {
            LastFetch = DateTime.Now;
        }
        /// <summary>
        /// When data was fetched
        /// </summary>
        public DateTime LastFetch { get; set; }

        /// <summary>
        /// Login of user
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Name of user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How many pull requests was made
        /// </summary>
        public int PullRequests { get; set; }
        /// <summary>
        /// How many commits was made
        /// </summary>
        public int Commits { get; set; }
        /// <summary>
        /// Commits made in repositories that user isn't the owner
        /// </summary>
        public int CommitsToAnotherRepositories { get; set; }
        /// <summary>
        /// How many pr was made for repositories that user isn't the owner
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
            var sumProducts = 
                   (PullRequests * rankPoints.PullRequests) +
                   (Commits * rankPoints.Commits) +
                   (PullRequestsToAnotherRepositories * rankPoints.PullRequestsToAnotherRepositories) +
                   (Issues * rankPoints.Issues) +
                   (CreatedRepositories * rankPoints.CreatedRepositories) +
                   (DirectStars * rankPoints.DirectStars) +
                   (IndirectStars * rankPoints.IndirectStars) +
                   (ContributedTo * rankPoints.ContributedTo) +
                   (ContributedToOwnRepositories * rankPoints.ContributedToOwnRepositories) +
                   (ContributedToNotOwnerRepositories * rankPoints.ContributedToNotOwnerRepositories) +
                   (Followers * rankPoints.Followers) +
                   (CommitsToAnotherRepositories * rankPoints.CommitsToAnotherRepositories)+
                   (CommitsToMyRepositories * rankPoints.CommitsToMyRepositories);

            return sumProducts; // / rankPoints.Total(Login);
        }

        static string FormatNumber(int num)
        {
            if (num >= 100000)
            {
                return FormatNumber(num / 1000) + "K";
            }
            if (num >= 10000)
            {
                return (num / 1000D).ToString("0.#") + "K";
            }
            if (num >= 1000)
            {
                return (num / 1000D).ToString("0.#") + "K";
            }
            return num.ToString("#,0");


        }

        public string TotalCommits() => FormatNumber(Commits);
        public string TotalStars() => FormatNumber(DirectStars + IndirectStars);
        public string TotalPullRequests() => FormatNumber(PullRequests);
        public string TotalIssues() => FormatNumber(Issues);
        public string TotalContributedFor() => FormatNumber(ContributedTo);
    }
}