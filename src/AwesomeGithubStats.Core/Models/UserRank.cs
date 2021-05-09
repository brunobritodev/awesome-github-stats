using System;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class UserRank
    {
        private readonly RankDegree _rankDegree;
        public RankPoints RankPoints { get; }
        public UserStats UserStats { get; }

        public UserRank(RankPoints rankPoints, UserStats userStats, RankDegree rankDegree)
        {
            _rankDegree = rankDegree;
            RankPoints = rankPoints;
            UserStats = userStats;
            CalculateRank();
        }

        private void CalculateRank()
        {
            var totalPoints = RankPoints.Total();
            var totalDegree = _rankDegree.Total();
            var userScore = UserStats.GetScore(RankPoints) / 100;
            var cdf = 100 - (Normalcdf(userScore, totalDegree, totalPoints) * 100);
            foreach (var degree in _rankDegree.OrderByDescending(b => b.Value))
            {
                if (cdf >= degree.Value)
                {
                    Score = cdf;
                    Level = degree.Key;
                    break;
                }
            }
        }

        public string Level { get; set; }
        public double Score { get; set; }


        private double Normalcdf(double mean, double sigma, double to)
        {
            var z = (to - mean) / Math.Sqrt(2 * sigma * sigma);
            var t = 1 / (1 + 0.3275911 * Math.Abs(z));
            var a1 = 0.254829592;
            var a2 = -0.284496736;
            var a3 = 1.421413741;
            var a4 = -1.453152027;
            var a5 = 1.061405429;
            var erf =
                1 - ((((a5 * t + a4) * t + a3) * t + a2) * t + a1) * t * Math.Exp(-z * z);
            var sign = 1;
            if (z < 0)
            {
                sign = -1;
            }
            return (1.0 / 2.0) * (1 + sign * erf);
        }
    }
}
