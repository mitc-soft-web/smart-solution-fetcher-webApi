namespace MITC_Smart_Solution.Persistence
{
    public class QueryNormalizer
    {
        public string NormalizeForGithub(string userQuery)
        {
            if (string.IsNullOrWhiteSpace(userQuery))
                return "code";

            string q = userQuery.Trim().ToLower();

          
            var qualifiers = new List<string>
            {
                "in:name",
                "in:description",
                "in:readme"
            };

            
            if (!q.Contains("language:"))
            {
                qualifiers.Add("language:*"); 
            }

            return $"{q} {string.Join(" ", qualifiers)}";
        }

    }
}
