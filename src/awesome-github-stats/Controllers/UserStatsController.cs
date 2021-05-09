using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
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

        [HttpGet("{username}"), ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public async Task<IActionResult> Get(string username, [FromQuery] UserStatsOptions options)
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


        [HttpGet("teste")]
        public IActionResult teste()
        {
            var coent = System.IO.File.ReadAllText(Path.Combine(_environment.ContentRootPath, @"content\", "circle.svg"));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(coent));
            return File(ms, "image/svg+xml; charset=utf-8");
        }
    }
}
