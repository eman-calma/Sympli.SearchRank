using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sympli.SearchRank.Core.Helpers.Abstracts
{
    public interface IHttpRequestHelper
    {
        public Task<string> Invoke(string url, string keywords);
    }
}
