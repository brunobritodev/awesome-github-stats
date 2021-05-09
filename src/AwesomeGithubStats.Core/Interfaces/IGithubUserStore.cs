using AwesomeGithubStats.Core.Models.Responses;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Interfaces
{
    public interface IGithubUserStore
    {
        Task<User> GetUserInformation(string username);
        Task<ContributionsCollection> GetUserInformationByYear(string username, int year);
    }
}
