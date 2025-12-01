using Microsoft.AspNetCore.Mvc;
using MITC_Smart_Solution.Infrastructure;
using MITC_Smart_Solution.Interface.Services;
using MITC_Smart_Solution.SearchSolutionAPI;
using System.Text.Json;

namespace MITC_Smart_Solution.Controllers
{
    //[Route("api/[controller]")]
    public class SearchController(
        SearchCategorizer categorizer, ISearchHistoryService searchHistoryService,
        ISmartSolutionGeneratorService smartSolution) : ControllerBase
    {
        private readonly SearchCategorizer _categorizer = categorizer ?? throw new ArgumentNullException(nameof(categorizer));
        private readonly ISearchHistoryService _searchHistoryService = searchHistoryService ?? throw new ArgumentNullException(nameof(searchHistoryService));
        private readonly ISmartSolutionGeneratorService _smartSolution = smartSolution ?? throw new ArgumentNullException(nameof(smartSolution));
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var category = _categorizer.Classify(query);
            var solutionResults = await _smartSolution.SmartSolutionProvider(category, query);

            var jsonResult = JsonSerializer.Serialize(solutionResults);
            await _searchHistoryService.SaveSearchHistory(query, category, jsonResult);

            return Ok(new
            {
                Query = query,
                Category = category,
                Results = solutionResults
            });

        }
    }
}
