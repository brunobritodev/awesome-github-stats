namespace AwesomeGithubStats.Core.Models
{
    class CacheKeys
    {
        public static string SvgKey(string svg) => $"FILE:SVG:{svg}";
    }
}
