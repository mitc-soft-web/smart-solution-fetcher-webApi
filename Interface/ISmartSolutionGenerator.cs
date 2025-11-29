namespace MITC_Smart_Solution.Interface
{
    public interface ISmartSolutionGenerator
    {
        public Task<List<Object>> SmartSolutionProvider(string category, string query);
    }
}
