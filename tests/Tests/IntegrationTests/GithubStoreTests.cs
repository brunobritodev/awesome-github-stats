using AwesomeGithubStats.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tests.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests
{
    public class GithubStoreTests : IClassFixture<Warmup>
    {
        private readonly IGithubUserStore _githubUserStore;

        public GithubStoreTests(Warmup services, ITestOutputHelper output)
        {
            _githubUserStore = services.Services.GetRequiredService<IGithubUserStore>();
        }

        [Fact]
        public async Task Should_Get_User_Years_Of_Contribution()
        {
            var years = await _githubUserStore.GetUserInformation("brunobritodev");

            years.Should().NotBeNull();
            years.ContributionsCollection.ContributionYears.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Get_User_Specific_Year_Information()
        {
            var years = await _githubUserStore.GetUserInformationByYear("brunobritodev", 2018);

            years.Should().NotBeNull();
            years.RestrictedContributionsCount.Should().BeGreaterThan(0);
            years.TotalCommitContributions.Should().BeGreaterThan(0);
            years.TotalRepositoryContributions.Should().BeGreaterThan(0);
            years.CommitContributionsByRepository.Should().NotBeEmpty();
        }
    }
}
