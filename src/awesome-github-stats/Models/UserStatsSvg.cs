using AwesomeGithubStats.Core.Models;
using Humanizer;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Models
{
    public class UserStatsSvg
    {
        private readonly string _file;
        private readonly RankDegree _rankDegree;

        public UserStatsSvg(string file, RankDegree rankDegree)
        {
            _file = Path.Combine(file, @"svgs\", "user-stats.svg");
            _rankDegree = rankDegree;
        }

        private void CalculateProgressBar(UserRank rank)
        {

            var slices = _rankDegree.Count;
            var slicesToTheEnd = _rankDegree.Count(c => c.Value > rank.Score);
            var sliceMinSize = (100.0 / slices) * (slices - slicesToTheEnd);
            var nextRank = _rankDegree.OrderBy(o => o.Value).FirstOrDefault(f => f.Value > rank.Score);
            if (nextRank.Key == null)
            {
                ProgressBar = 100;
                return;

            }
            var rankSize = nextRank.Value - _rankDegree[rank.Level];
            var pointsInActualRank = rank.Score - _rankDegree[rank.Level];

            var percentualInActualRank = pointsInActualRank / rankSize;
            var sliceSize = 100.0 / slices;
            var slicePartToAdd = sliceSize * percentualInActualRank;

            ProgressBar = sliceMinSize + slicePartToAdd;
        }

        public double ProgressBar { get; set; }

        public async Task<Stream> Svg(UserRank rank, Styles styles)
        {
            CalculateProgressBar(rank);
            var fs = await File.ReadAllTextAsync(_file);
            var svgFinal = fs
                .Replace("{{Name}}", rank.UserStats.Name.Truncate(30))
                .Replace("{{ProgressBarStart}}", $"{ CalculateCircleProgress(0):F}")
                .Replace("{{ProgressBarEnd}}", $"{ CalculateCircleProgress(ProgressBar):F}")
                .Replace("{{TextColor}}", styles.TextColor)
                .Replace("{{TitleColor}}", styles.TitleColor)
                .Replace("{{IconColor}}", styles.IconColor)
                .Replace("{{ShowIcons}}", styles.ShowIcons ? "block" : "none")
                .Replace("{{Stars}}", rank.UserStats.TotalStars())
                .Replace("{{Commits}}", rank.UserStats.TotalCommits())
                .Replace("{{PRS}}", rank.UserStats.TotalPullRequests())
                .Replace("{{Issuers}}", rank.UserStats.TotalIssues())
                .Replace("{{Contributions}}", rank.UserStats.TotalContributedFor())
                .Replace("{{Level}}", rank.Level);


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
