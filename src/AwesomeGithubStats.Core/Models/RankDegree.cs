using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        public double MaxPoints { get; set; } = 20000;

        public KeyValuePair<string, int> InRange(double value)
        {
            foreach (var range in this.OrderByDescending(o => o.Value))
            {
                var percent = range.Value * MaxPoints / 100;
                if (value >= percent)
                    return range;
            }
        }
    }
}
