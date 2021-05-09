using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        public double MaxPoints { get; set; } = 20000;
        public int Total()
        {
            return this.Values.Sum();
        }
    }
}
