using AwesomeGithubStats.Core.Models;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface IRankService
    {
        UserRank CalculateRank(UserStats userStats);
    }
}