using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace mParticle.LoadGenerator
{
    public class WebRequestHandler : IWebRequestHandler
    {
        private HttpClient httpClient;
        private string url;
        public WebRequestHandler(HttpClient httpClient, string url)
        {
            this.httpClient = httpClient;
            this.url = url;
        }
        public Task<int> MakeRequest()
        {
            string requestPayload = "{ \"name\": \"YOUR_NAME\", \"date\": \"" + DateTime.UtcNow + "\", \"requests_sent\": 0 }";
            Task<HttpResponseMessage> responseTask = httpClient.PostAsync(url, new StringContent(requestPayload));
            return responseTask.ContinueWith(response => response.Result.IsSuccessStatusCode ? 200 : 500);
        }
    }
}