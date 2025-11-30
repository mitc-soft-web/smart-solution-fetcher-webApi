namespace MITC_Smart_Solution.Interface
{
    public interface ISmartSolutionGeneratorService
    {
        public Task<List<Object>> SmartSolutionProvider(string category, string query);
    }
}
