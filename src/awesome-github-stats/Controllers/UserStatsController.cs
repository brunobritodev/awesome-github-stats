using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeGithubStats.Controllers
{
    [ApiController]
    [Route("user-stats")]
    public class UserStatsController : ControllerBase
    {
        private readonly ILogger<UserStatsController> _logger;

        public UserStatsController(ILogger<UserStatsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            return Ok("hello world");
        }
    }
}
