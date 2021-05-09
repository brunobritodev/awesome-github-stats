using AwesomeGithubStats.Core.Models;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface IGithubService
    {
        Task<UserStats> GetUserStats(string username);
    }

}
