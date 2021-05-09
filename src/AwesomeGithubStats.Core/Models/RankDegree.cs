using System.Collections.Generic;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        public double MaxPoints { get; set; } = 20000;

        public bool InRange(double value)
        {

        }
    }
}
