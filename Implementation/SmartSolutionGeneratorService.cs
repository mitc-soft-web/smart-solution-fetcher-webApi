using MITC_Smart_Solution.Interface;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace MITC_Smart_Solution.Implementation
{
    public class SmartSolutionGeneratorService(HttpClient httpClient, IConfiguration configuration) : ISmartSolutionGenerator
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

       public async Task<List<object>> SmartSolutionProvider(string category, string query)
        {
            var results = new List<object>();

            if (category == "software" || category == "webdesign")
            {
                results.Add(await StackOverflow(query));
                results.Add(await GithubRepos(query));
                results.Add(await GithubGists(query));
                results.Add(await DevTo(query));
                results.Add(await NugetPackages(query));
                results.Add(await NpmPackages(query));
                results.Add(await YouTube(query));
                results.Add(await LinkedInJobs(query));
            }
            else
            {
                results.Add(await DuckDuckGo(query));
                results.Add(await Wikipedia(query));
                results.Add(await YouTube(query));
            }

            return results;
        }


        private async Task<object> StackOverflow(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://api.stackexchange.com/2.3/search?order=desc&sort=activity&intitle={q}&site=stackoverflow");
        }
       

        private async Task<object> GithubRepos(string q)
        {
            string githubTokenKey = _configuration["MITCSmartSolution:GithubTokenKey"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubTokenKey);
            return await _httpClient.GetFromJsonAsync<object>($"https://api.github.com/search/repositories?q={q}");
        }

        private async Task<object> GithubGists(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://api.github.com/search/code?q={q}");
        }

        private async Task<object> DevTo(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://dev.to/api/articles?per_page=5&tag={q}");
        }
            

        private async Task<object> NugetPackages(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://api.nuget.org/v3/query?q={q}");
        }

        private async Task<object> NpmPackages(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://registry.npmjs.org/-/v1/search?text={q}");
        }
            

        private async Task<object> YouTube(string q)
        {
            string youtubeApiKey = _configuration["MITCSmartSolution:YoutubeApiKey"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://youtube.googleapis.com/youtube/v3/search?part=snippet&q={q}&key={youtubeApiKey}");
        }

        private async Task<object> LinkedInJobs(string q)
        {
            string linkedInApiKey = _configuration["MITCSmartSolution:LinkedInApiKey"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return new { message = "LinkedIn Jobs API requires OAuth. Provide token.", query = q };
        }

        private async Task<object> DuckDuckGo(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://api.duckduckgo.com/?q={q}&format=json");
        }
           

        private async Task<object> Wikipedia(string q)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"https://en.wikipedia.org/api/rest_v1/page/summary/{q}");
        }
            
    }

}
