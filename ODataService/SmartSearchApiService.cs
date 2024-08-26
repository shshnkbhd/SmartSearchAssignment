using Microsoft.Extensions.Configuration;

namespace ODataService
{
    public class SmartSearchApiService
    {
        private IConfiguration _configuration;
        protected string UserName => _configuration.GetSection("ApiCredentials").GetValue<string>("UserName");
        protected string Password => _configuration.GetSection("ApiCredentials").GetValue<string>("Password");
        protected string ApiKey => _configuration.GetSection("ApiCredentials").GetValue<string>("ApiKey");
        protected string BaseUrl => _configuration.GetSection("ApiCredentials").GetValue<string>("BaseUrl");

        private SmartSearchApiService() { }

        public SmartSearchApiService(IConfiguration configuration) { 
            _configuration = configuration;
        }
    }
}
