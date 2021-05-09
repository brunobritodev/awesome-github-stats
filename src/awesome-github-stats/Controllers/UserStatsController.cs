using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Controllers
{
    [ApiController]
    [Route("user-stats")]
    public class UserStatsController : ControllerBase
    {
        private readonly ILogger<UserStatsController> _logger;
        private readonly IGithubService _githubService;
        private readonly IRankService _rankService;
        private readonly IWebHostEnvironment _environment;
        private RankDegree _degree;

        public UserStatsController(ILogger<UserStatsController> logger,
            IGithubService githubService,
            IRankService rankService,
            IWebHostEnvironment environment,
            IOptions<RankDegree> rankDegree)
        {
            _degree = rankDegree.Value;
            _logger = logger;
            _githubService = githubService;
            _rankService = rankService;
            _environment = environment;
        }

        [HttpGet("{username}"), ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public async Task<IActionResult> Get(string username)
        {
            var svg = new UserStatsSvg(Path.Combine(_environment.ContentRootPath, @"svgs\", "user-stats.svg"), _degree);
            var rank = _rankService.CalculateRank(new UserStats()
            {
                Login = "brunohbrito",
                Name = "Bruno Brito",
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
            var content = await svg.Svg(rank, new Styles());

            return File(content, "image/svg+xml; charset=utf-8");
        }
    }
}
