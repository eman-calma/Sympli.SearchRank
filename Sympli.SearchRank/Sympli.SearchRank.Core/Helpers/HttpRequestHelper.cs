using RestSharp;
using Sympli.SearchRank.Core.Helpers.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sympli.SearchRank.Core.Helpers
{
    public class HttpRequestHelper : IHttpRequestHelper
    {
        public async Task<string> Invoke(string url, string keywords)
        {
            var maxPage = 9;
            var searchResult = string.Empty;
            try
            {

                for (var i = 0; i <= maxPage; i++)
                {
                    int page = i * 10;
                    
                    string requestUrl = string.Format(url, HttpUtility.UrlEncode(keywords), page);
                    
                    var client = new RestClient(requestUrl);
                    client.Timeout = -1;
                    
                    var request = new RestRequest(Method.GET);
                    
                    IRestResponse response = await client.ExecuteAsync(request);

                    searchResult += response.Content;

                }
                return searchResult;
            }
            catch (Exception ex)
            {
                //Implement custom logging here
                return null;
            }
        }
    }
}
