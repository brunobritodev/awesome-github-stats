using AwesomeGithubStats.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Models
{
    public class UserStatsSvg
    {
        private readonly UserRank _rank;
        private readonly string _file;
        private readonly RankDegree _rankDegree;

        public UserStatsSvg(UserRank rank, string file, RankDegree rankDegree)
        {
            _rank = rank;
            _file = file;
            _rankDegree = rankDegree;
        }

        private void CalculateProgressBar()
        {

            var slices = _rankDegree.Count;
            var slicesToTheEnd = _rankDegree.Count(c => c.Value > _rank.Score);
            var sliceMinSize = (100.0 / slices) * (slices - slicesToTheEnd);
            var nextRank = _rankDegree.OrderBy(o => o.Value).First(f => f.Value > _rank.Score);

            var rankSize = nextRank.Value - _rankDegree[_rank.Level];
            var pointsInActualRank = _rank.Score - _rankDegree[_rank.Level];

            var percentualInActualRank = pointsInActualRank / rankSize;
            var sliceSize = 100.0 / slices;
            var slicePartToAdd = sliceSize * percentualInActualRank;

            ProgressBar = sliceMinSize + slicePartToAdd;
        }

        public double ProgressBar { get; set; }

        public async Task<Stream> Svg(Styles styles)
        {
            CalculateProgressBar();
            var fs = await File.ReadAllTextAsync(_file);
            var svgFinal = fs
                .Replace("{{Name}}", _rank.UserStats.Name)
                .Replace("{{ProgressBarStart}}", $"{ CalculateCircleProgress(0):F}")
                .Replace("{{ProgressBarEnd}}", $"{ CalculateCircleProgress(0):F}")
                .Replace("{{TextColor}}", styles.TextColor)
                .Replace("{{TitleColor}}", styles.TitleColor)
                .Replace("{{IconColor}}", styles.IconColor)
                .Replace("{{ShowIcons}}", styles.ShowIcons ? "block" : "none");


            return new MemoryStream(Encoding.UTF8.GetBytes(svgFinal));
        }

        private double CalculateCircleProgress(double value)
        {
            var radius = 40;
            var c = Math.PI * (radius * 2);

            if (value < 0) value = 0;
            if (value > 100) value = 100;

            var percentage = ((100 - value) / 100) * c;
            return percentage;
        }
    }
}
