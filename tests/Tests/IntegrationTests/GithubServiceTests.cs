using AwesomeGithubStats.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;
using Tests.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests
{
    public class GithubServiceTests : IClassFixture<Warmup>
    {
        private readonly ITestOutputHelper _output;
        private readonly IGithubService _githubService;

        public GithubServiceTests(Warmup services, ITestOutputHelper output)
        {
            _output = output;
            _githubService = services.Services.GetRequiredService<IGithubService>();
        }

        [Theory]
        //[InlineData("brunobritodev")]
        [InlineData("sindresorhus")]
        [InlineData("kamranahmedse")]
        [InlineData("ralmsdeveloper")]
        [InlineData("anuraghazra")]
        [InlineData("eduardopires")]
        public async Task Should_Get_User_Information(string username)
        {
            var stats = await _githubService.GetUserStats(username);

            _output.WriteLine($"Stats: {JsonSerializer.Serialize(stats, new JsonSerializerOptions() { WriteIndented = true })}");
            stats.Should().NotBeNull();
            stats.Commits.Should().BeGreaterThan(0);
        }

    }
}