using Microsoft.Extensions.Configuration;
using Sympli.SearchRank.Core.Helpers.Abstracts;

namespace Sympli.SearchRank.Core.Helpers
{
    public class SearchEngineHelper : ISearchEngineHelper
    {
        private string searchUrl;
        public IConfiguration _configuration { get; }

        public SearchEngineHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSearchUrl(string searchEngine)
        {
            switch (searchEngine.ToLower())
            {
                case "google":
                    searchUrl = _configuration["SearchEngines:Google:URL"];
                    break;
                case "bing":
                    searchUrl = _configuration["SearchEngines:Bing:URL"];
                    break;
                default:
                    searchUrl = string.Empty;
                    break;
            }

            return searchUrl;
        }
    }
}
