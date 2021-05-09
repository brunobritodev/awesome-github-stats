using System.Collections.Generic;
using System.Linq;

namespace AwesomeGithubStats.Core.Models
{
    public class RankDegree : Dictionary<string, int>
    {
        public int Total()
        {
            return this.Values.Sum();
        }
    }
}
