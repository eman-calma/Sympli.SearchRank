using Microsoft.Extensions.Configuration;
using Sympli.SearchRank.Core.Helpers.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.SearchRank.Core.Helpers
{
    public class ExpressionMatchHelper : IExpressionMatchHelper
    {
        private string expression;
        public IConfiguration _configuration { get; }

        public ExpressionMatchHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetExpression(string searchEngine)
        {
            switch (searchEngine.ToLower())
            {
                case "google":
                    expression = _configuration["SearchEngines:Google:MatchingExpression"];
                    break;
                case "bing":
                    expression = _configuration["SearchEngines:Bing:MatchingExpression"];
                    break;
            }

            return expression;
        }
    }
}
