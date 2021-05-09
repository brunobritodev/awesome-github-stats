using System.Collections.Generic;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        public double MaxPoints { get; set; } = 20000;

        public KeyValuePair<string, int> InRange(double value)
        {
            foreach (var range in this)
            {

            }
        }
    }
}
