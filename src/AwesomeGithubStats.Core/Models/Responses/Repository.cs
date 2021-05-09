using System.Diagnostics;

namespace AwesomeGithubStats.Core.Models.Responses
{
    [DebuggerDisplay("{NameWithOwner}")]
    public class Repository
    {
        public string NameWithOwner { get; set; }
        public int StargazerCount { get; set; }
    }
}