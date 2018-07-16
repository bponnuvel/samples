using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Resilience.Http;

namespace SampleApp2.Services
{
    public class AndroidService
    {

        private readonly IHttpClient _apiClient;
        private readonly string _androidApiBaseUrl;
        public AndroidService(IHttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _apiClient = httpClient;
            _androidApiBaseUrl = appSettings.Value.AndroidApiBaseUri;
        }

        public async Task<string[]> GetVersions()
        {
            var url = $"{_androidApiBaseUrl}/android";

            var dataString = await _apiClient.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<string[]>(dataString);

            return response;

        }


    }
}

