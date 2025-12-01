using MITC_Smart_Solution.Entities;
using MITC_Smart_Solution.Interface.Repositories;
using MITC_Smart_Solution.Interface.Services;

namespace MITC_Smart_Solution.Implementation.Services
{
    public class SearchHistoryService(
        ISearchHistoryRepository searchHistoryRepository, ILogger<SearchHistoryService> logger) : ISearchHistoryService
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository = searchHistoryRepository ?? throw new ArgumentNullException(nameof(searchHistoryRepository));
        private readonly ILogger<SearchHistoryService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public Task<bool> SaveSearchHistory(string query, string category, string jsonResult)
        {
            if (string.IsNullOrWhiteSpace(query) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(jsonResult))
            {
                _logger.LogWarning("Fields cannot be empty.");
                return Task.FromResult(false);
            }

            var addSearchHistory = new SearchHistory(query, category, jsonResult);
            return _searchHistoryRepository.SaveSearchHistory(addSearchHistory);
        }
    }
}
