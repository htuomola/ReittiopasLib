using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReittiopasLib.Model;

namespace ReittiopasLib
{
    public class ReittiopasClient
    {
        private const string Username = "";
        private const string Password = "";
        private const string BaseUri = "http://api.reittiopas.fi/hsl/prod/?";

        private HttpClient _httpClient;

        public ReittiopasClient()
        {
            _httpClient = new HttpClient();
        }

        public ReittiopasClient(HttpMessageHandler httpHandler)
        {
            _httpClient = new HttpClient(httpHandler);
        }

        public async Task<IEnumerable<Place>> Geocode(string keyword)
        {
            var parameters = new Dictionary<string, string>
                {
                    {"request", "geocode"},
                    {"key", Uri.EscapeDataString(keyword)}
                };

            Uri uri = BuildRequestUri(parameters);
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);
            // TODO exception handling
            httpResponseMessage.EnsureSuccessStatusCode();
            var stringContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var places = await JsonConvert.DeserializeObjectAsync<IEnumerable<Place>>(stringContent);
            return places;
        }

        private static Uri BuildRequestUri(Dictionary<string, string> parameters)
        {
            var defaultParams = new Dictionary<string, string>
                {
                    {"user", Username},
                    {"pass", Password},
                    {"epsg_in", "wgs84"},
                    {"epsg_out", "wgs84"}
                };

            var queryParams = from p in parameters.Concat(defaultParams)
                        select p.Key + "=" + p.Value;
            var query = string.Join("&", queryParams);

            return new Uri(BaseUri + query);
        }

        //public IEnumerable<Route> FindRoute(FindRouteRequest request)
        //{
            
        //}
 
        //public IEnumerable<Stop> FindStops(FindStopsRequest request)
        //{
            
        //}
    }
}