using AwesomeGithubStats.Core.Interfaces;
using AwesomeGithubStats.Core.Models;
using AwesomeGithubStats.Core.Models.Responses;
using Polly;
using Polly.Retry;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AwesomeGithubStats.Core.Store
{
    class GithubUserStore : IGithubUserStore
    {
        private readonly HttpClient _client;

        private readonly AsyncRetryPolicy _policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1)
            }, (exception, timeSpan) =>
            {
                Log.Error($"Error trying to get data: {exception}");
            });
        public GithubUserStore(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("github");
        }
        public async Task<User> GetUserInformation(string username)
        {
            var request = new DefaultRequest()
            {
                Query = GithubOptions.UserAllTimeInformation,
                OperationName = GithubOptions.OperationName,
                Variables = new { login = username }
            };
            var response = await _policy.ExecuteAndCaptureAsync(() => _client.PostAsJsonAsync("graphql", request, GithubOptions.DefaultJson));

            if (response.Outcome != OutcomeType.Successful || !response.Result.IsSuccessStatusCode) return null;
            var content = await response.Result.Content.ReadAsStringAsync();
            var userInfo = await JsonSerializer.DeserializeAsync<DefaultResponse<UserData>>(await response.Result.Content.ReadAsStreamAsync(), GithubOptions.DefaultJson);

            return userInfo?.Data.User;

        }

        public async Task<ContributionsCollection> GetUserInformationByYear(string username, int year)
        {
            var request = new DefaultRequest()
            {
                Query = GithubOptions.ContributionsFromYear.Replace("{year}", $"{year}").Replace("{next_year}", (year + 1).ToString()),
                OperationName = GithubOptions.OperationName,
                Variables = new { login = username }
            };
            var response = await _policy.ExecuteAndCaptureAsync(() => _client.PostAsJsonAsync("graphql", request, GithubOptions.DefaultJson));

            if (response.Outcome != OutcomeType.Successful || !response.Result.IsSuccessStatusCode) return null;


            var userInfo = await JsonSerializer.DeserializeAsync<DefaultResponse<UserData>>(await response.Result.Content.ReadAsStreamAsync(), GithubOptions.DefaultJson);
            userInfo.Data.User.ContributionsCollection.Year = year;
            return userInfo?.Data.User.ContributionsCollection;

        }

    }
}
