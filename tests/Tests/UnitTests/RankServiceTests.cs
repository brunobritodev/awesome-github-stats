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
        private Mock<IOptions<RankPoints>> _rankPoints;

        public RankServiceTests()
        {
            _rankPoints = new Mock<IOptions<RankPoints>>();
            var rankDegree = new Mock<IOptions<RankDegree>>();
            _rankPoints.Setup(s => s.Value).Returns(new RankPoints());
            rankDegree.Setup(s => s.Value).Returns(new RankDegree()
                {
                    new(){Rank = "S++",Points = 300000},
                    new(){Rank = "S+",Points =  63000},
                    new(){Rank = "S",Points =  32000},
                    new(){Rank = "A++",Points =  21000},
                    new(){Rank = "A+",Points =  14000},
                    new(){Rank = "A",Points =  7000},
                    new(){Rank = "💪",Points = 0}
                }
            );

            _rankService = new RankService(_rankPoints.Object, rankDegree.Object);
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
                CommitsToAnotherRepositories = 5,
                CommitsToMyRepositories = 182,
            });
            rank.Level.Should().Be("💪");
            rank.Score.Should().Be(4780);
        }

        [Fact]
        public void Should_Calculate_Rank_SPlusPlus()
        {
            _rankPoints.Setup(s => s.Value).Returns(new RankPoints()
            {
                Commits = 1,
                ContributedToNotOwnerRepositories = 10,
                ContributedToOwnRepositories = 1,
                CreatedRepositories = 1,
                DirectStars = 3.5,
                Followers = 1,
                IndirectStars = 1,
                Issues = 1,
                PullRequests = 1,
                PullRequestsToAnotherRepositories = 5,
                CommitsToMyRepositories = 1,
                CommitsToAnotherRepositories = 10,
                ContributedTo = 1,
            });

            var rank = _rankService.CalculateRank(new UserStats()
            {
                Login = "sindresorhus",
                Name = "Sindre Sorhus",
                Commits = 25353,
                ContributedTo = 296,
                ContributedToNotOwnerRepositories = 157,
                ContributedToOwnRepositories = 136,
                CreatedRepositories = 616,
                DirectStars = 424912,
                Followers = 42719,
                IndirectStars = 1053005,
                Issues = 4146,
                PullRequests = 1400,
                PullRequestsToAnotherRepositories = 849,
                CommitsToAnotherRepositories = 22526,
                CommitsToMyRepositories = 2827
            });
            rank.Level.Should().Be("S++");
            rank.Score.Should().BeGreaterOrEqualTo(69884);
        }

        [Fact]
        public void Should_Calculate_Rank_S()
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
                CommitsToAnotherRepositories = 51,
                CommitsToMyRepositories = 365
            });
            rank.Level.Should().Be("S");
            rank.Score.Should().BeGreaterOrEqualTo(58937);
        }


        [Fact]
        public void Should_Be_Rank_SPlus()
        {
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Commits = 2262,
                ContributedTo = 99,
                ContributedToNotOwnerRepositories = 43,
                ContributedToOwnRepositories = 56,
                CreatedRepositories = 145,
                DirectStars = 24934,
                Followers = 3701,
                IndirectStars = 184586,
                Issues = 115,
                PullRequests = 455,
                PullRequestsToAnotherRepositories = 251,
                CommitsToAnotherRepositories = 1181,
                CommitsToMyRepositories = 1081,

            });
            rank.Level.Should().Be("S+");
            rank.Score.Should().BeGreaterOrEqualTo(58937);
        }


        [Fact]
        public void Should_Calculate_Rank_APlus()
        {
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Commits = 576,
                CommitsToAnotherRepositories = 26,
                ContributedTo = 22,
                CommitsToMyRepositories = 56,
                ContributedToNotOwnerRepositories = 9,
                ContributedToOwnRepositories = 13,
                CreatedRepositories = 23,
                DirectStars = 5296,
                Followers = 2787,
                IndirectStars = 7821,
                Issues = 60,
                PullRequests = 19,
                PullRequestsToAnotherRepositories = 10,
            });
            rank.Level.Should().Be("A++");
            rank.Score.Should().BeGreaterOrEqualTo(29997);
        }
    }
}
