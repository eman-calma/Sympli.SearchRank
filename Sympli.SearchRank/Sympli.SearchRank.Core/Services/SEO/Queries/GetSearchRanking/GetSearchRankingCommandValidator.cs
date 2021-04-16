using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sympli.SearchRank.Core.Services.SEO.Queries.GetSearchRanking
{
    public class GetSearchRankingCommandValidator : AbstractValidator<GetSearchRankingCommand>
    {
        public GetSearchRankingCommandValidator()
        {
            RuleFor(x => x.Keywords).MaximumLength(100).NotEmpty().NotNull(); ;
            RuleFor(x => x.URL).MaximumLength(100).NotEmpty().NotNull();
            RuleFor(x => x.SearchEngine).MaximumLength(100).NotEmpty().NotNull().Must(isSearchEngineValid)
              .WithMessage("Invalid Search Engine");
        }

        private bool isSearchEngineValid(string searchEngine)
        {
            var searchEngineList = new List<string>() { "google", "bing" };
            return searchEngineList.Contains(searchEngine.ToLower());
        }
    }
}
