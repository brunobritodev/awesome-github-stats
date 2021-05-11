using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Options;
using AwesomeGithubStats.Core.Models.Svgs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Api.Controllers
{
    [ApiController]
    [Route("user-stats")]
    public class UserStatsController : ControllerBase
    {
        private readonly ILogger<UserStatsController> _logger;
        private readonly IGithubService _githubService;
        private readonly IRankService _rankService;
        private readonly ISvgService _svgService;
        private readonly IWebHostEnvironment _environment;

        public UserStatsController(ILogger<UserStatsController> logger,
            IGithubService githubService,
            IRankService rankService,
            ISvgService svgService,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _githubService = githubService;
            _rankService = rankService;
            _svgService = svgService;
            _environment = environment;
        }

        [HttpGet("{username}"), ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Get(string username, [FromQuery] UserStatsOptions options)
        {
            var userStats = await _githubService.GetUserStats(username);
            var rank = _rankService.CalculateRank(userStats);
            var content = await _svgService.GetUserStatsImage(rank, options);


            return File(content, "image/svg+xml; charset=utf-8");
        }

        /// <summary>
        /// Do not use this endpoint. It exists to prevent preview page bugs
        /// </summary>
        [HttpGet("{username}/preview")]
        public async Task<IActionResult> Preview(string username, [FromQuery] UserStatsOptions options)
        {
            var userStats = await _githubService.GetUserStats(username);
            var rank = _rankService.CalculateRank(userStats);
            var content = await _svgService.GetUserStatsImage(rank, options);


            return File(content, "image/svg+xml; charset=utf-8");
        }
        [HttpGet("{username}/stats")]
        public async Task<IActionResult> GetStats(string username)
        {
            var userStats = await _githubService.GetUserStats(username);
            return Ok(userStats);
        }


        [HttpGet("{username}/rank")]
        public async Task<IActionResult> GetRank(string username)
        {
            var userStats = await _githubService.GetUserStats(username);
            var rank = _rankService.CalculateRank(userStats);
            return Ok(rank);
        }

#if DEBUG
        [HttpGet("show/{card}")]
        public IActionResult Teste(string card, [FromQuery] UserStatsOptions options)
        {
            if (!System.IO.File.Exists(Path.Combine(_environment.ContentRootPath, @"svgs\user-stats", $"{card}.svg")))
            {
                return BadRequest();
            }

            var coent = System.IO.File.ReadAllText(Path.Combine(_environment.ContentRootPath, @"svgs\user-stats", $"{card}.svg"));

            var usercard = new UserStatsCard(new RankDegree()
            {
                new(){Rank = "S++",Points = 300000},
                new(){Rank = "S+",Points =  63000},
                new(){Rank = "S",Points =  28000},
                new(){Rank = "A++",Points =  21000},
                new(){Rank = "A+",Points =  14000},
                new(){Rank = "A",Points =  7000},
                new(){Rank = "💪",Points = 0}
            }, options);
            var rank = _rankService.CalculateRank(new()
            {
                Name = "Sindre Sorhus",
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
            var style = new CardStyles();
            style.Apply(options);
            return File(usercard.Svg(coent, rank, style, new CardTranslations()), "image/svg+xml");
        }
#endif
    }
}
