using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReittiopasLib.Dto;
using ReittiopasLib.Model;

namespace ReittiopasLib
{
    public class ReittiopasClient
    {
        private const string BaseUri = "http://api.reittiopas.fi/hsl/prod/?";
        
        private readonly string _username;
        private readonly string _password;
        private readonly HttpClient _httpClient;

        public ReittiopasClient(string apiUsername, string apiPassword)
            : this(apiUsername, apiPassword, new HttpClientHandler())
        {
        }

        public ReittiopasClient(string apiUsername, string apiPassword, HttpMessageHandler httpHandler)
        {
            _username = apiUsername;
            _password = apiPassword;
            _httpClient = new HttpClient(httpHandler);
            var defaultSettings = new JsonSerializerSettings()
                {
                    Converters = new[] { new HslDateTimeConverter() },
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                };
            JsonConvert.DefaultSettings = () => defaultSettings;
        }

        public async Task<IEnumerable<Place>> Geocode(string keyword)
        {
            var parameters = new Dictionary<string, string>
                {
                    {"key", keyword}
                };

            Uri uri = BuildRequestUri("geocode", parameters);
            var places = await GetDataAsync<IEnumerable<PlaceDto>>(uri);
            return places.Select(DataMapper.MapPlace);
        }

        public async Task<IEnumerable<Place>> ReverseGeocode(Coordinates coordinates,
            PlaceKind kind = PlaceKind.Address, int limit = 3, int radius = 1000)
        {
            var parameters = new Dictionary<string, string>
                {
                    {"radius", radius.ToString(CultureInfo.InvariantCulture)},
                    {"limit", limit.ToString(CultureInfo.InvariantCulture)},
                    {"result_contains", kind.ToString("G").ToLowerInvariant()},
                };

            Uri uri = BuildRequestUri("reverse_geocode", parameters);
            var results = await GetDataAsync<IEnumerable<PlaceDto>>(uri);
            return results.Select(DataMapper.MapPlace);
        }

        public async Task<IEnumerable<Route>> FindRoute(RouteParameters routeParams)
        {
            var parameters = new Dictionary<string, string>
                {
                    {"from", routeParams.From.ToString()},
                    {"to", routeParams.To.ToString()},  
                };

            Uri uri = BuildRequestUri("route", parameters);
            var results = await GetDataAsync<IEnumerable<IEnumerable<RouteDto>>>(uri);
            // API retuns an array which has a single array
            return results.SelectMany(r => r).Select(DataMapper.MapRoute);
        }

        private async Task<T> GetDataAsync<T>(Uri uri)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);
            // TODO exception handling
            httpResponseMessage.EnsureSuccessStatusCode();
            var stringContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var places = await JsonConvert.DeserializeObjectAsync<T>(stringContent);
            return places;
        }

        //public IEnumerable<Route> FindRoute(FindRouteRequest request)
        //{

        //}

        //public IEnumerable<Stop> FindStops(FindStopsRequest request)
        //{

        //}

        private Uri BuildRequestUri(string requestType, Dictionary<string, string> parameters)
        {
            var defaultParams = new Dictionary<string, string>
                {
                    {"request", requestType},
                    {"user", _username},
                    {"pass", _password},
                    {"epsg_in", "wgs84"},
                    {"epsg_out", "wgs84"}
                };

            var queryParams = from p in parameters.Concat(defaultParams)
                              select p.Key + "=" + Uri.EscapeUriString(p.Value);
            var query = string.Join("&", queryParams);

            return new Uri(BaseUri + query);
        }
    }
}