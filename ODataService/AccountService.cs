using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ODataService.Models;

namespace ODataService
{
    public class AccountService : SmartSearchApiService
    {
        private static Account _account = new Account();

        public AccountService(IConfiguration configuration) : base(configuration){}

        private async Task<Account> GetAccount() {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/accounts");
            request.Headers.Add("X-API-KEY", ApiKey);
            request.Headers.Add("Accept", "application/json;odata.metadata=minimal;odata.streaming=true");
            var stringContent = $@"{{
                                  ""password"": ""{Password}"",
                                  ""userName"": ""{UserName}""
                                }}";
            var content = new StringContent(stringContent, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            _account = JsonConvert.DeserializeObject<Account>(responseContent);

            return _account;
        }

        public async Task<Account> AuthenticateAccount() {
            if (DateTime.Now >= _account.ExpiresIn || string.IsNullOrWhiteSpace(_account.AccessToken)) {
                _account = await GetAccount();
            }

            return _account;
        }
    }
}
