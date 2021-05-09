using AwesomeGithubStats.Core.Models;
using System.IO;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface ISvgService
    {
        Task<Stream> GetUserStatsImage(UserRank rank, UserStatsOptions options);
    }
}
