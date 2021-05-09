using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Svgs;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Services
{
    class SvgService : ISvgService
    {
        private readonly string _contentRoot;
        private readonly ICacheService _cacheService;
        private RankDegree _degree;

        public SvgService(string contentRoot, ICacheService cacheService, IOptions<RankDegree> rankDegree)
        {
            _degree = rankDegree.Value;
            _contentRoot = contentRoot;
            _cacheService = cacheService;
        }

        public Task<Stream> GetUserStatsImage(UserRank rank)
        {
            var svg = new UserStatsSvg(_contentRoot, _degree);

            var content = await svg.Svg(rank, new Styles());
        }
    }
}
