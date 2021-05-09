using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Svgs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TimeSpan = System.TimeSpan;

namespace AwesomeGithubStats.Core.Services
{
    class SvgService : ISvgService
    {
        private readonly string _contentRoot;
        private readonly ICacheService _cacheService;
        private readonly RankDegree _degree;
        private string SvgFolder { get; set; }
        private string TranslationFolder { get; set; }

        public SvgService(
            IWebHostEnvironment environment,
            ICacheService cacheService,
            IOptions<RankDegree> rankDegree)
        {
            _degree = rankDegree.Value;
            _contentRoot = environment.ContentRootPath;
            _cacheService = cacheService;
            SvgFolder = Path.Combine(_contentRoot, @"\svgs");
            TranslationFolder = Path.Combine(_contentRoot, @"\translations");
        }



        public async Task<Stream> GetUserStatsImage(UserRank rank, UserStatsOptions options)
        {
            var file = await GetSvgFile("user-stats.svg");
            var svg = new UserStatsCard(file, _degree);

            var translations = options.HasTranslations() ? await GetTranslationsFile(options.Locale) : await GetTranslationsFile("en");

            return svg.Svg(rank, new CardStyles(), translations);
        }

        private async Task<CardTranslations> GetTranslationsFile(string language)
        {
            var file = Path.Combine(TranslationFolder, $"{language.ToLower()}.json");
            if (!File.Exists(file))
                file = Path.Combine(TranslationFolder, "en.json"); ;

            var translations = _cacheService.Get<CardTranslations>(CacheKeys.TranslationKey(language));
            if (translations != null)
                return translations;

            var jsonContent = await File.ReadAllTextAsync(file);
            translations = JsonSerializer.Deserialize<CardTranslations>(jsonContent);

            _cacheService.Set(CacheKeys.TranslationKey(language), translations, TimeSpan.FromDays(30));

            return translations;
        }
        private async Task<string> GetSvgFile(string file)
        {
            var svgContent = _cacheService.Get<string>(CacheKeys.SvgKey(file));
            if (!string.IsNullOrEmpty(svgContent))
                return svgContent;

            svgContent = await File.ReadAllTextAsync(Path.Combine(SvgFolder, "user-stats.svg"));

            _cacheService.Set(CacheKeys.SvgKey(file), svgContent, TimeSpan.FromDays(30));

            return svgContent;
        }
    }
}
