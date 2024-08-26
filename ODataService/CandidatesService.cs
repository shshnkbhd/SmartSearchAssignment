using Microsoft.Extensions.Configuration;
using Microsoft.OData.Client;
using ODataService.Models;
using SmartSearch.OpenApi.Models;

namespace ODataService
{
    public class CandidatesService : SmartSearchApiService
    {
        private Account _account;
        private AccountService _accountService;

        public CandidatesService(IConfiguration configuration) : base(configuration) { }

        public CandidatesService(AccountService accountService, IConfiguration configuration) : this(configuration) {
            _accountService = accountService;
        }

        public async Task<IEnumerable<CandidateResponse>> GetCandidatesByZipCode(string zipCode) {
            _account = await _accountService.AuthenticateAccount();

            if (_account.ExpiresIn == DateTime.MinValue) {
                throw new Exception("Could not authenticate with API");
            }

            var oDataUri = new Uri(BaseUrl, UriKind.Absolute);
            var context = new Default.Container(oDataUri);

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(SendingRequest2);

            return context.Candidates.Where(c => c.CandidatePostalCode == zipCode);
        }

        private void SendingRequest2(object sender, SendingRequest2EventArgs e) {
            e.RequestMessage.SetHeader("Authorization", $"Bearer {_account.AccessToken}");
            e.RequestMessage.SetHeader("Accept", "application/json;odata.metadata=minimal;odata.streaming=true");
        }
    }
}
