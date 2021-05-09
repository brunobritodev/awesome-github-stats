using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {


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
