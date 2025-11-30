using MITC_Smart_Solution.Interface;
using MITC_Smart_Solution.Persistence;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace MITC_Smart_Solution.Implementation
{
    public class SmartSolutionGeneratorService(HttpClient httpClient, 
        IConfiguration configuration) : ISmartSolutionGeneratorService
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly QueryNormalizer _queryNormalizer = new QueryNormalizer();

       public async Task<List<object>> SmartSolutionProvider(string category, string query)
        {
            var results = new List<object>();

            if (category == "software" || category == "webdesign")
            {
                var tasks = new List<Task<object>>()
                {
                    SafeFetch(() => StackOverflow(query)),
                    //SafeFetch(() => GithubRepos(query)),
                    SafeFetch(() => GithubGists(query)),
                    SafeFetch(() => DevTo(query)),
                    SafeFetch(() => NugetPackages(query)),
                    SafeFetch(() => NpmPackages(query)),
                    SafeFetch(() => YouTube(query)),
                    // SafeFetch(() => LinkedInJobs(query))
                };

                var resultsArray = await Task.WhenAll(tasks);
                results.AddRange(resultsArray);
            }
            else
            {
                var tasks = new List<Task<object?>>()
                {
                    SafeFetch(() => DuckDuckGo(query)),
                    SafeFetch(() => Wikipedia(query)),
                    SafeFetch(() => YouTube(query))
                };

                var resultsArray = await Task.WhenAll(tasks);
                results.AddRange(resultsArray);
            }

            return results.Where(r => r != null).ToList();

        }


        private async Task<object> StackOverflow(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:StackOverflow"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object?>($"{url}={q}&site=stackoverflow");
        }
       

        //private async Task<object> GithubRepos(string q)
        //{
        //    var baseUrl = _configuration["MITCSmartSolution:SmartSolutionUrls:GithubRepos"];
        //    string githubTokenKey = _configuration["MITCSmartSolution:GithubTokenKey"];

        //    string safeQuery = _queryNormalizer.NormalizeForGithub(q);

        //    _httpClient.DefaultRequestHeaders.UserAgent.Clear();
        //    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");

        //    if (!string.IsNullOrWhiteSpace(githubTokenKey))
        //    {
        //        _httpClient.DefaultRequestHeaders.Authorization =
        //            new AuthenticationHeaderValue("Bearer", githubTokenKey);
        //    }

        //    string url = $"{baseUrl}?q={Uri.EscapeDataString(safeQuery)}";

        //    var response = await _httpClient.GetAsync(url);
        //    response.EnsureSuccessStatusCode();

        //    return await response.Content.ReadFromJsonAsync<object>();
        //}

        private async Task<object> GithubGists(string q)
        {
            string githubTokenKey = _configuration["MITCSmartSolution:GithubTokenKey"];
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:GithubGists"];

            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");

            if (!string.IsNullOrWhiteSpace(githubTokenKey))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", githubTokenKey);
            }
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<object?>();
        }

        private async Task<object> DevTo(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:DevTo"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"{url}={q}");
        }


        private async Task<object> NugetPackages(string q)
        {
            var baseUrl = _configuration["MITCSmartSolution:SmartSolutionUrls:NugetPackages"];
            if (string.IsNullOrWhiteSpace(q)) return null;

            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");

            string url = $"{baseUrl}={Uri.EscapeDataString(q)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<object?>();
        }

        private async Task<object> NpmPackages(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:NpmPackages"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"{url}={q}");
        }
            

        private async Task<object> YouTube(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:YouTube"];
            string youtubeApiKey = _configuration["MITCSmartSolution:YoutubeApiKey"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"{url}={q}&key={youtubeApiKey}");
        }

        //private async Task<object> LinkedInJobs(string q)
        //{
        //    string linkedInApiKey = _configuration["MITCSmartSolution:LinkedInApiKey"];
        //    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
        //    return new { message = "LinkedIn Jobs API requires OAuth. Provide token.", query = q };
        //}

        private async Task<object> DuckDuckGo(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:DuckDuckGo"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"{url}={q}&format=json");
        }
           

        private async Task<object> Wikipedia(string q)
        {
            var url = _configuration["MITCSmartSolution:SmartSolutionUrls:Wikipedia"];
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("mitc-smart-solution");
            return await _httpClient.GetFromJsonAsync<object>($"{url}{q}");
        }

        private async Task<object?> SafeFetch(Func<Task<object?>> apiCall)
        {
            try
            {
                return await apiCall();
            }
            catch (HttpRequestException ex)
            {
                
                Console.WriteLine($"API fetch failed: {ex.Message}");
                return null; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }


    }

}
