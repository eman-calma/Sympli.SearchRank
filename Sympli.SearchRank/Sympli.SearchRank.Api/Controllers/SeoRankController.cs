using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sympli.SearchRank.Core.Services.SEO.Queries.GetSearchRanking;

namespace Sympli.SearchRank.Api.Controllers
{
    public class SeoRankController : BaseController
    {
       [HttpPost]
        public async Task<IActionResult> GetRanking([FromBody] GetSearchRankingCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
