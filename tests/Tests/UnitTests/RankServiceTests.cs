using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Tests.UnitTests
{
    public class RankServiceTests
    {
        private RankService _rankService;

        public RankServiceTests()
        {
            var rankPoints = new Mock<IOptions<RankPoints>>();
            var rankDegree = new Mock<IOptions<RankDegree>>();
            rankPoints.Setup(s => s.Value).Returns(new RankPoints()
            {
                Commits = 1,
                ContributedToNotOwnerRepositories = 2.30,
                ContributedToOwnRepositories = 0.50,
                Issues = 1,
                DirectStars = 1.75,
                IndirectStars = 0.75,
                PullRequests = 0.5,
                Followers = 1,
                CreatedRepositories = 0.5,
                PullRequestsToAnotherRepositories = 2,
                ContributedTo = 1
            });
            rankDegree.Setup(s => s.Value).Returns(new RankDegree()
                {
                    {"S+", 99},
                    {"S", 75},
                    {"A++", 65},
                    {"A+", 40},
                    {"A", 20},
                    {"💪",0}
                }

            );

            _rankService = new RankService(rankPoints.Object, rankDegree.Object);
        }

        [Fact]
        public void Should_Calculate_Rank_SPlus()
        {
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Commits = 25353,
                ContributedTo = 296,
                ContributedToNotOwnerRepositories = 157,
                ContributedToOwnRepositories = 136,
                CreatedRepositories = 616,
                DirectStars = 424912,
                Followers = 42719,
                IndirectStars = 0,
                Issues = 4146,
                Login = "sindresorhus",
                Name = "Sindre Sorhus",
                PullRequests = 1400,
                PullRequestsToAnotherRepositories = 849,

            });
            rank.Level.Should().Be("S+");
            rank.Score.Should().Be(100);
        }

        [Fact]
        public void Should_Calculate_Rank()
        {
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Commits = 4674,
                ContributedTo = 49,
                ContributedToNotOwnerRepositories = 13,
                ContributedToOwnRepositories = 36,
                CreatedRepositories = 53,
                DirectStars = 1366,
                Followers = 375,
                IndirectStars = 48472,
                Issues = 57,
                PullRequests = 210,
                PullRequestsToAnotherRepositories = 20,

            });
            rank.Level.Should().Be("S+");
            rank.Score.Should().Be(100);
        }

        [Fact]
        public void Should_Calculate_Rank_C()
        {
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Commits = 187,
                ContributedTo = 24,
                ContributedToNotOwnerRepositories = 0,
                ContributedToOwnRepositories = 24,
                CreatedRepositories = 26,
                DirectStars = 520,
                Followers = 2467,
                IndirectStars = 0,
                Issues = 0,
                PullRequests = 0,
                PullRequestsToAnotherRepositories = 0,

            });
            rank.Level.Should().Be("💪");
            rank.Score.Should().Be(100);
        }
    }
}
