using AwesomeGithubStats.Core.Models.Svgs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AwesomeGithubStats.Core.Models.Options;

namespace AwesomeGithubStats.Core.Util
{
    static class CustomExtensions
    {
        public static IEnumerable<TSource> DistinctByProperty<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }


        public static bool IsMissing(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static CardTranslations Language(this IEnumerable<CardTranslations> translations, [NotNull] string language) => translations.FirstOrDefault(f => f.Locale.ToLower().Equals(language.ToLower()));
        public static CardStyles Theme(this IEnumerable<CardStyles> translations, [NotNull] string theme) => translations.FirstOrDefault(f => f.Theme.ToLower().Equals(theme.ToLower()));

    }
}
