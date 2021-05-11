using AwesomeGithubStats.Core.Models.Options;
using Humanizer;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AwesomeGithubStats.Core.Models.Svgs
{
    public class UserStatsCard
    {
        private readonly UserStatsOptions _options;
        private const int Radius = 60;
        /// <summary>
        /// It should be the same of SVG
        /// </summary>
        private const double ProgressBarWidth = 175;
        private readonly string CircleProgressBar = $@"
<circle class=""rank-circle-rim"" cx=""-10"" cy=""8"" r=""{Radius}""/>
<circle class=""rank-circle"" cx=""-10"" cy=""8"" r=""{Radius}"" />
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
                .Replace("{{Name}}", rank.UserStats.Name.Truncate(25))
                .Replace("{{Stars}}", rank.UserStats.TotalStars())
                .Replace("{{Commits}}", rank.UserStats.TotalCommits())
                .Replace("{{PRS}}", rank.UserStats.TotalPullRequests())
                .Replace("{{Issuers}}", rank.UserStats.TotalIssues())
                .Replace("{{Contributions}}", rank.UserStats.TotalContributedFor())
                .Replace("{{Level}}", rank.Level)
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
            var width = ProgressBarWidth;
            var size = value * width / 100D;
            return size;
        }
        private double CalculateCircleProgress(double value)
        {
            var degrees = 360D;
            var size = value * degrees / 100D;
            return size;
        }

        public string GetCardName()
        {
            return _options.CardType.ToLower() switch
            {
                "default" => "level-card.svg",
                "level" => "level-card.svg",
                "level-alternate" => "level-alt-card.svg",
                "octocat" => "octocat-card.svg",
                "github" => "github-card.svg",
                _ => "level-card.svg"
            };
        }
    }
}
