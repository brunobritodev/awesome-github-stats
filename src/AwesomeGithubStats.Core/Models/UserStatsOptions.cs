namespace AwesomeGithubStats.Core.Models
{
    public class UserStatsOptions
    {
        public string Locale { get; set; } = "en";
        public string Theme { get; set; } = "default";
        public string TextColor { get; set; }
        public string TitleColor { get; set; }
        public string IconColor { get; set; }
        public bool? ShowIcons { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
    }

}
