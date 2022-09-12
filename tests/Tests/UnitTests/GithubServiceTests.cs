using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Tests.Fakes;
using Xunit;

namespace Tests.UnitTests
{
    public class GithubServiceTests
    {
        private readonly GithubService _githubService;
        private readonly Mock<IGithubUserStore> _githubUserStore;
        private readonly Mock<ICacheService> _cacheService;

        public GithubServiceTests()
        {
            _githubUserStore = new Mock<IGithubUserStore>();
            _cacheService = new Mock<ICacheService>();

            _githubService = new GithubService(_githubUserStore.Object, _cacheService.Object);
        }

        [Fact]
        public async Task Should_Get_User_Stats()
        {
            _cacheService.Setup(s => s.Get<UserStats>(It.IsAny<string>())).Returns((UserStats)null);
            _githubUserStore.Setup(s => s.GetUserInformationByYear(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(UserDataFaker.GetFixedContributionsFromYear);
            _githubUserStore.Setup(s => s.GetUserInformation(It.IsAny<string>())).ReturnsAsync(UserDataFaker.GetFixedUserInformation());

            var stats = await _githubService.GetUserStats("brunobritodev");
            stats.Name.Should().Be("Bruno Brito");
            stats.Commits.Should().Be(1457);
            stats.ContributedTo.Should().Be(26);
            stats.CreatedRepositories.Should().Be(10);
            stats.DirectStars.Should().Be(1136);
            stats.IndirectStars.Should().Be(5315);
            stats.Issues.Should().Be(45);
        }
    }
}
