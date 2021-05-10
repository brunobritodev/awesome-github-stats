using Humanizer;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AwesomeGithubStats.Core.Models.Svgs
{
    public class UserStatsCard
    {
        private readonly UserStatsOptions _options;

        private const string CircleProgressBar = @"
<circle class=""rank-circle-rim"" cx=""-10"" cy=""8"" r=""50""/>
<circle class=""rank-circle"" cx=""-10"" cy=""8"" r=""50"" />
";

        public RankDegree RankDegree { get; }

        public UserStatsCard(RankDegree rankDegree, UserStatsOptions options)
        {
            _options = options;
            RankDegree = rankDegree;
        }

        private void CalculateProgressBar(UserRank rank)
        {

            var slices = RankDegree.TotalSlices();
            var slicesToTheEnd = RankDegree.CountSlicesAfter(rank.Score);
            var sliceMinSize = (100.0 / slices) * (slices - slicesToTheEnd);
            var nextRank = RankDegree.OrderBy(o => o.Points).FirstOrDefault(f => f.Points > rank.Score);
            if (nextRank == null)
            {
                ProgressBar = 100;
                ShowCircleProgressBar = false;
                return;

            }

            var rankSize = nextRank.Points - RankDegree[rank.Level];
            var pointsInActualRank = rank.Score - RankDegree[rank.Level];

            var percentualInActualRank = pointsInActualRank / rankSize;
            var sliceSize = 100.0 / slices;
            var slicePartToAdd = sliceSize * percentualInActualRank;

            ProgressBar = sliceMinSize + slicePartToAdd;
            if (ProgressBar > 100)
                ProgressBar = 100;
            if (ProgressBar < 0)
                ProgressBar = 0;
        }

        public bool ShowCircleProgressBar { get; set; } = true;
        public double ProgressBar { get; set; }

        public Stream Svg(string file, UserRank rank, CardStyles cardStyles, CardTranslations cardTranslations)
        {
            CalculateProgressBar(rank);
            var svgFinal = file
                .Replace("{{Name}}", rank.UserStats.Name.Truncate(30))
                .Replace("{{Stars}}", rank.UserStats.TotalStars())
                .Replace("{{Commits}}", rank.UserStats.TotalCommits())
                .Replace("{{PRS}}", rank.UserStats.TotalPullRequests())
                .Replace("{{Issuers}}", rank.UserStats.TotalIssues())
                .Replace("{{Contributions}}", rank.UserStats.TotalContributedFor())
                .Replace("{{Level}}", rank.Level)
                .Replace("{{ProgressBarStart}}", $"{CalculateCircleProgress(0):F}")
                .Replace("{{ProgressBarEnd}}", $"{CalculateCircleProgress(ProgressBar):F}")
                // Translations
                .Replace("{{StarsLabel}}", cardTranslations.StarsLabel)
                .Replace("{{PullRequestLabel}}", cardTranslations.PullRequestLabel)
                .Replace("{{IssuesLabel}}", cardTranslations.IssuesLabel)
                .Replace("{{CommitsLabel}}", cardTranslations.CommitsLabel)
                .Replace("{{ContributionsLabel}}", cardTranslations.ContributionsLabel)
                // Theme
                .Replace("{{TextColor}}", cardStyles.TextColor)
                .Replace("{{TitleColor}}", cardStyles.TitleColor)
                .Replace("{{IconColor}}", cardStyles.IconColor)
                .Replace("{{BackgroundColor}}", cardStyles.BackgroundColor)
                .Replace("{{BorderColor}}", cardStyles.BorderColor)
                .Replace("{{RingColor}}", cardStyles.RingColor)
                .Replace("{{ShowIcons}}", cardStyles.ShowIcons ? "block" : "none")
                .Replace("{{TextPosition}}", cardStyles.ShowIcons ? "25" : "0")
                .Replace("{{ProgressBarWidth}}", CalculateRectangleProgress(ProgressBar).ToString(CultureInfo.InvariantCulture))
                ;


            if (ShowCircleProgressBar)
            {
                svgFinal = svgFinal
                    .Replace("{{ProgressBar}}", CircleProgressBar);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(svgFinal));
        }


        private double CalculateRectangleProgress(double value)
        {
            var width = 110D;
            var percentage = value * width / 100D;
            return percentage;
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

        public string GetCardName()
        {
            return _options.CardType.ToLower() switch
            {
                "default" => "level-card.svg",
                "level" => "level-card.svg",
                "octocat" => "octocat-card.svg",
                "github" => "github-card.svg",
                _ => "level-card.svg"
            };
        }
    }
}
