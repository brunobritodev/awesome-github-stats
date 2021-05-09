using AwesomeGithubStats.Core.Models;
using System.IO;
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
            foreach (var degre in _rankDegree)
            {

            }
        }

        public async Task<Stream> Svg()
        {
            var fs = await File.ReadAllTextAsync(_file);
            var svgFinal = fs.Replace("{{Name}}", _rank.UserStats.Name);

            return new MemoryStream(Encoding.UTF8.GetBytes(svgFinal));
        }
    }
}
