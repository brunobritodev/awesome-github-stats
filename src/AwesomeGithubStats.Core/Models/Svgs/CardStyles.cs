namespace AwesomeGithubStats.Core.Models.Svgs
{
    public class CardStyles
    {
        public string TextColor { get; set; } = "#333";
        public string TitleColor { get; set; } = "#2f80ed";
        public string IconColor { get; set; } = "#4c71f2";
        public bool ShowIcons { get; set; } = true;
        public string Theme { get; set; } = "default";
        public string BackgroundColor { get; set; } = "#fffefe";
        public string BorderColor { get; set; } = "#e4e2e2";

        public void Apply(UserStatsOptions options)
        {
            TextColor = options.TextColor ?? TextColor;
            TitleColor = options.TitleColor ?? TitleColor;
            IconColor = options.IconColor ?? IconColor;
            ShowIcons = options.ShowIcons ?? ShowIcons;
            BackgroundColor = options.BackgroundColor ?? BackgroundColor;
            BorderColor = options.BorderColor ?? BorderColor;
        }
    }
}
