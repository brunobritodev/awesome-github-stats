namespace AwesomeGithubStats.Core.Models
{
    class CacheKeys
    {
        public static string SvgKey(string svg) => $"FILE:SVG:{svg}";
        public static string TranslationKey(string language) => $"FILE:TRANSLATIONS:{language}";
    }
}
