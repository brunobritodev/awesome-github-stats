namespace AwesomeGithubStats.Core.Models
{
    public class UserStatsOptions
    {
        public string Locale { get; set; } = "en";
        public string Theme { get; set; } = "default";
        public string Text { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool? ShowIcons { get; set; }
        public string Background { get; set; }
        public string Border { get; set; }
        public string Ring { get; set; }
        public string CardType { get; set; } = "default";
    }

}
