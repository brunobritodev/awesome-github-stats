using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        /// <summary>
        /// This score is based in user sindresorhus. An astonishing developer with many, many contributions.
        /// His score is 70.000. But it's impossible to someone get 90% of that. So, I got 10% and everyone who is 90% of him will get S+
        /// </summary>
        public double MaxPoints { get; set; } = 7000;

        public KeyValuePair<string, int> InRange(double value)
        {

            foreach (var range in this.OrderByDescending(o => o.Value))
            {
                var percent = range.Value * MaxPoints / 100;
                if (value >= percent)
                    return range;
            }

            return this.OrderByDescending(o => o.Value).Last();
        }
    }
}
