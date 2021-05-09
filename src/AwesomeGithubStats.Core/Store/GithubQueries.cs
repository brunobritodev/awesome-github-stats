using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace AwesomeGithubStats.Core.Store
{
    public static class GithubOptions
    {
        public static JsonSerializerOptions DefaultJson = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase

        };
        public static Dictionary<string, long> PersonalAccessTokenUsage { get; set; } = new();

        public const string OperationName = "userInfo";
        public const string UserAllTimeInformation = @"
            query userInfo($login: String!) {
                user(login: $login) {
                    name
                    login
                    contributionsCollection {
                        contributionYears
                    }
                    pullRequests(first: 1) {
                          totalCount
                    }
                    issues(first: 1) {
                      totalCount
                    }
                    followers {
                      totalCount
                    }
                }
            }
";
        public const string ContributionsFromYear = @"
            query userInfo($login: String!) {
                user(login: $login) {
                    contributionsCollection(from: ""{year}-01-01T00:00:00Z"", to: ""{next_year}-01-01T00:00:00Z"") {
                        totalCommitContributions
                        totalRepositoryContributions
                        restrictedContributionsCount
                        pullRequestContributionsByRepository{
                          contributions{
                            totalCount
                          }
                          repository {
                            nameWithOwner
                            stargazerCount
                          }
                        }
                        commitContributionsByRepository{
                          contributions {
                            totalCount
                          }
                          repository{
                            nameWithOwner
                            stargazerCount
                          }
                        }
                    }
                }
            }
";

        public static string NextPat()
        {
            var pat = PersonalAccessTokenUsage.OrderByDescending(o => o.Value).First();
            var patValue = pat.Value;
            PersonalAccessTokenUsage[pat.Key] = Interlocked.Increment(ref patValue);
            return pat.Key;
        }
    }
}