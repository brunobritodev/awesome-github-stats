using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using Microsoft.Extensions.Options;

namespace AwesomeGithubStats.Core.Services
{
    public class RankService : IRankService
    {
        private readonly RankPoints _rankPoints;
        private readonly RankDegree _rankDegree;

        public RankService(IOptions<RankPoints> points, IOptions<RankDegree> rankDegree)
        {
            _rankPoints = points.Value;
            _rankDegree = rankDegree.Value;
        }

        public UserRank CalculateRank(UserStats userStats)
        {
            return new(_rankPoints, userStats, _rankDegree);
        }
    }
}
