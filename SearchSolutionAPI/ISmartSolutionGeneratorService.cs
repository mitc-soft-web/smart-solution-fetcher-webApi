namespace MITC_Smart_Solution.SearchSolutionAPI
{
    public interface ISmartSolutionGeneratorService
    {
        public Task<List<Object>> SmartSolutionProvider(string category, string query);
    }
}
