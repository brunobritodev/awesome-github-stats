using AwesomeGithubStats.Core.Util;

namespace AwesomeGithubStats.Core.Models
{
    public class UserStatsOptions
    {
        public string Locale { get; set; }
        public bool HasTranslations()
        {
            return Locale.IsPresent();
        }

    }

}
