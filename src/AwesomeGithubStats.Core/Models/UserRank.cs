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

        /// <summary>
        /// This score is based in user sindresorhus. An astonishing developer with many, many contributions.
        /// His score is 2,094,175.5 and weighted score is 70.000.
        /// </summary>
        private void CalculateRank()
        {
            var weightedScore = UserStats.GetScore(RankPoints);

            var degree = _rankDegree.InRange(weightedScore);
            Score = weightedScore;
            Level = degree.Rank;
        }

        public string Level { get; set; }
        public double Score { get; set; }

    }
}
