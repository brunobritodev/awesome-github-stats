using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeGithubStats.Core.Interfaces;

namespace AwesomeGithubStats.Core.Services
{
    class SvgService : ISvgService
    {
        private readonly string _contentRoot;
        private readonly ICacheService _cacheService;

        public SvgService(string contentRoot, ICacheService cacheService)
        {
            _contentRoot = contentRoot;
            _cacheService = cacheService;
        }

        
    }
}
