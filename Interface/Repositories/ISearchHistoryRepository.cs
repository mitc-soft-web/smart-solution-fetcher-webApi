using Microsoft.EntityFrameworkCore;
using MITC_Smart_Solution.Entities;

namespace MITC_Smart_Solution.Interface.Repositories
{
    public interface ISearchHistoryRepository
    {
        public Task<List<SearchHistory>> RecentSearch();

        public Task<bool> SaveSearchHistory(SearchHistory searchHistory);
    }
}
