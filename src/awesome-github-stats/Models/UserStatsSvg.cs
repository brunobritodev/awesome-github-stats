using AwesomeGithubStats.Core.Models;

namespace AwesomeGithubStats.Models
{
    public class UserStatsSvg
    {
        private readonly UserRank _rank;

        public UserStatsSvg(UserRank rank)
        {
            _rank = rank;
        }

        public string Svg()
        {

        }
    }
}
