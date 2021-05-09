using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        /// <summary>
        /// This score is based in user sindresorhus. An astonishing developer with many, many contributions.
        /// His weighted score is 70.000.
        /// </summary>
        public double MaxPoints { get; set; } = 70000;

        public KeyValuePair<string, int> InRange(double value)
        {

            foreach (var range in this.OrderByDescending(o => o.Value))
            {
                if (value >= range.Value)
                    return range;
            }

            return this.OrderByDescending(o => o.Value).Last();
        }
    }
}
