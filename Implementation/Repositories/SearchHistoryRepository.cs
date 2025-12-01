using Microsoft.EntityFrameworkCore;
using MITC_Smart_Solution.Context;
using MITC_Smart_Solution.Entities;
using MITC_Smart_Solution.Interface;
using MITC_Smart_Solution.Interface.Repositories;

namespace MITC_Smart_Solution.Implementation.Repositories
{
    public class SearchHistoryRepository(SmartSolutionContext context) : ISearchHistoryRepository
    {
        private readonly SmartSolutionContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<SearchHistory>> RecentSearch()
        {
            return await _context.Set<SearchHistory>()
                .OrderByDescending(sh => sh.SearchDate)
                .Take(20)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> SaveSearchHistory(SearchHistory searchHistory)
        {
           await _context.Set<SearchHistory>().AddAsync(searchHistory);
           var result = await _context.SaveChangesAsync();
            return result > 0;
        }


    }
}
