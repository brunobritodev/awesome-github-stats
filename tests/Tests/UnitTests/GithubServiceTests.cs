using AwesomeGithubStats.Core.Interfaces;
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

        public GithubServiceTests()
        {
            _githubUserStore = new Mock<IGithubUserStore>();

            _githubService = new GithubService(_githubUserStore.Object);
        }

        [Fact]
        public async Task Should_Get_User_Stats()
        {
            _githubUserStore.Setup(s => s.GetUserInformationByYear(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(UserDataFaker.GetFixedContributionsFromYear);
            _githubUserStore.Setup(s => s.GetUserInformation(It.IsAny<string>())).ReturnsAsync(UserDataFaker.GetFixedUserInformation());

            var stats = await _githubService.GetUserStats("brunohbrito");
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
