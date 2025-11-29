using Microsoft.EntityFrameworkCore;
using MITC_Smart_Solution.Entities;

namespace MITC_Smart_Solution.Context
{
    public class SmartSolutionContext(DbContextOptions<SmartSolutionContext> options): DbContext(options)
    {
        DbSet<SearchHistory> SearchHistories => Set<SearchHistory>();
    }
}
