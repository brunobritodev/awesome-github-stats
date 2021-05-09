namespace AwesomeGithubStats.Core.Models
{
    class CacheKeys
    {
        public static string SvgKey(string svg) => $"FILE:Svg:{svg}";
        public static string TranslationKey => $"FILE:Translations";
        public static string StyleKey => $"FILE:Style";
    }
}
