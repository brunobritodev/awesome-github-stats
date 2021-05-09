using AwesomeGithubStats.Core.Models;
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

            CalculateProgressBar();
        }

        private void CalculateProgressBar()
        {
            var slices = _rankDegree.Count;
            var slicesToTheEnd = _rankDegree.Count(c => c.Value > _rank.Score);
            var sliceMinSize = (100.0 / slices) * (slices - slicesToTheEnd);
        }

        public async Task<Stream> Svg()
        {
            var fs = await File.ReadAllTextAsync(_file);
            var svgFinal = fs.Replace("{{Name}}", _rank.UserStats.Name);

            return new MemoryStream(Encoding.UTF8.GetBytes(svgFinal));
        }
    }
}
