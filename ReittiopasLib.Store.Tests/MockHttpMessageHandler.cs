using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ReittiopasLib.Store.Tests
{
    public class MockHttpMessageHandler : HttpClientHandler
    {
        private readonly string _response;

        public MockHttpMessageHandler(string response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_response)
                };
            return Task.FromResult(response);
        }
    }
}