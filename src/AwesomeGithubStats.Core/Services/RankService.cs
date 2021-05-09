using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using Microsoft.Extensions.Options;

namespace AwesomeGithubStats.Core.Services
{
    public class RankService : IRankService
    {
        private RankPoints _rankPoints;
        private RankDegree _rankDegree;

        public RankService(IOptions<RankPoints> points, IOptions<RankDegree> rankDegree)
        {
            _rankPoints = points.Value;
            _rankDegree = rankDegree.Value;
        }

        public UserRank CalculateRank(UserStats userStats)
        {

            return new UserRank(_rankPoints, userStats, _rankDegree);
        }
    }
}
