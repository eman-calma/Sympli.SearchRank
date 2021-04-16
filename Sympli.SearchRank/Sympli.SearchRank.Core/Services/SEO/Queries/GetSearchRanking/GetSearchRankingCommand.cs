using MediatR;
using Microsoft.Extensions.Configuration;
using Sympli.SearchRank.Common.Models;
using Sympli.SearchRank.Core.Helpers.Abstracts;
using Sympli.SearchRank.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sympli.SearchRank.Core.Services.SEO.Queries.GetSearchRanking
{
    public class GetSearchRankingCommand : IRequest<ResponseModel>
    {
        public string SearchEngine { get; set; }
        public string Keywords { get; set; }
        public string URL { get; set; }

        public class Handler : IRequestHandler<GetSearchRankingCommand, ResponseModel>
        {
            private IHttpRequestHelper _httpRequestHelper;
            private ISearchEngineHelper _searchEngineHelper;
            private IConfiguration _configuration;
            private ICacheData<string> _cacheData;
            private IExpressionMatchHelper _expressionMatchHelper;

            public Handler(IHttpRequestHelper httpRequestHelper, ISearchEngineHelper searchEngineHelper, IConfiguration configuration, ICacheData<string> cacheData, IExpressionMatchHelper expressionMatchHelper)
            {
                _httpRequestHelper = httpRequestHelper;
                _searchEngineHelper = searchEngineHelper;
                _configuration = configuration;
                _cacheData = cacheData;
                _expressionMatchHelper = expressionMatchHelper;
            }

            public async Task<ResponseModel> Handle(GetSearchRankingCommand request, CancellationToken cancellationToken)
            {
                var result = new SeoRankModel();
                result.RankResult = new List<string>();
                var searchResultHTML = string.Empty;

                try
                {
                    var searchEngineUrl = _searchEngineHelper.GetSearchUrl(request.SearchEngine);

                    searchResultHTML = await _cacheData.GetOrCreate($"{request.SearchEngine}_{request.Keywords}", async () => await _httpRequestHelper.Invoke(searchEngineUrl, request.Keywords));

                    if (searchResultHTML != null)
                    {
                        string matchingExpression = _expressionMatchHelper.GetExpression(request.SearchEngine);  

                        MatchCollection matches = Regex.Matches(searchResultHTML, matchingExpression);

                        for (int x = 0; x < matches.Count; x++)
                        {
                            string match = matches[x].Value;
                            if (match.Contains(request.URL))
                            {
                                result.RankResult.Add((x + 1).ToString());
                            }
                        }

                        if (result.RankResult.Count == 0)
                        {
                            result.RankResult.Add("0");
                        }
                    }

                }
                catch (Exception ex)
                {
                    return new ResponseModel() { Success = false, Message = "An error has occured.", Data = null };
                }

                return new ResponseModel() { Success = true, Message = "Search result ranking request completed.", Data = result }; 
            }
        }
    }
}
