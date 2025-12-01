namespace MITC_Smart_Solution.Interface.Services
{
    public interface ISearchHistoryService
    {
        Task<bool> SaveSearchHistory(string query, string category, string jsonResult);
    }
}
