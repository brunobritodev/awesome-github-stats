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
            var weightedScore = UserStats.GetScore(RankPoints);

            var degree = _rankDegree.InRange(weightedScore);
            Score = weightedScore;
            Level = degree.Key;
        }

        public string Level { get; set; }
        public double Score { get; set; }
    }
}
