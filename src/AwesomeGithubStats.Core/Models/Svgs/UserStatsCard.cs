using Humanizer;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AwesomeGithubStats.Core.Models.Svgs
{
    public class UserStatsCard
    {
        public string File { get; }
        public RankDegree RankDegree { get; }

        public UserStatsCard(string file, RankDegree rankDegree)
        {
            File = file;
            RankDegree = rankDegree;
        }

        private void CalculateProgressBar(UserRank rank)
        {

            var slices = RankDegree.Count;
            var slicesToTheEnd = RankDegree.Count(c => c.Value > rank.Score);
            var sliceMinSize = (100.0 / slices) * (slices - slicesToTheEnd);
            var nextRank = RankDegree.OrderBy(o => o.Value).FirstOrDefault(f => f.Value > rank.Score);
            if (nextRank.Key == null)
            {
                ProgressBar = 100;
                return;

            }
            var rankSize = nextRank.Value - RankDegree[rank.Level];
            var pointsInActualRank = rank.Score - RankDegree[rank.Level];

            var percentualInActualRank = pointsInActualRank / rankSize;
            var sliceSize = 100.0 / slices;
            var slicePartToAdd = sliceSize * percentualInActualRank;

            ProgressBar = sliceMinSize + slicePartToAdd;
        }

        public double ProgressBar { get; set; }

        public Stream Svg(UserRank rank, CardStyles cardStyles, CardTranslations cardTranslations)
        {
            CalculateProgressBar(rank);
            var svgFinal = File
                .Replace("{{Name}}", rank.UserStats.Name.Truncate(30))
                .Replace("{{ProgressBarStart}}", $"{ CalculateCircleProgress(0):F}")
                .Replace("{{ProgressBarEnd}}", $"{ CalculateCircleProgress(ProgressBar):F}")
                .Replace("{{TextColor}}", cardStyles.TextColor)
                .Replace("{{TitleColor}}", cardStyles.TitleColor)
                .Replace("{{IconColor}}", cardStyles.IconColor)
                .Replace("{{ShowIcons}}", cardStyles.ShowIcons ? "block" : "none")
                .Replace("{{Stars}}", rank.UserStats.TotalStars())
                .Replace("{{Commits}}", rank.UserStats.TotalCommits())
                .Replace("{{PRS}}", rank.UserStats.TotalPullRequests())
                .Replace("{{Issuers}}", rank.UserStats.TotalIssues())
                .Replace("{{Contributions}}", rank.UserStats.TotalContributedFor())
                .Replace("{{Level}}", rank.Level)
                // Translations
                .Replace("{{StarsLabel}}", cardTranslations.StarsLabel)
                .Replace("{{PullRequestLabel}}", cardTranslations.PullRequestLabel)
                .Replace("{{IssuesLabel}}", cardTranslations.IssuesLabel)
                .Replace("{{CommitsLabel}}", cardTranslations.CommitsLabel)
                .Replace("{{ContributionsLabel}}", cardTranslations.ContributionsLabel);




            return new MemoryStream(Encoding.UTF8.GetBytes(svgFinal));
        }

        private double CalculateCircleProgress(double value)
        {
            var radius = 50;
            var c = Math.PI * (radius * 2);

            if (value < 0) value = 0;
            if (value > 100) value = 100;

            var percentage = ((100 - value) / 100) * c;
            return percentage;
        }
    }
}
