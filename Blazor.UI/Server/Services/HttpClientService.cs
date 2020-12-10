using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.UI.Server.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly string baseUrl;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HttpClientService> _log;
        public HttpClientService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<HttpClientService> log)
        {
            _clientFactory = clientFactory;
            baseUrl = configuration["ApiUrl"];
            _log = log;
        }

        public async Task<HttpResponseMessage> PostRequest<T>(T command, string actionUrl) where T : class
        {
            var postUrl = $"{baseUrl}/{actionUrl}";
            _log.LogInformation("Post request {0}", postUrl);

            var client = _clientFactory.CreateClient();
            var postContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(postUrl, postContent);

            _log.LogInformation("Post Request {0} done", postUrl);
            return result;
        }
        public async Task<T> GetRequest<T>(T contectResult, string contentUrl) where T : class
        {
            var getUrl = $"{baseUrl}/{ contentUrl}";
            _log.LogInformation("Get request {0}", getUrl);

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(getUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                contectResult = JsonConvert.DeserializeObject<T>(content);

                _log.LogInformation("Get request {0} done", getUrl);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new Exception(content);
            }
            return contectResult;
        }
    }
}
