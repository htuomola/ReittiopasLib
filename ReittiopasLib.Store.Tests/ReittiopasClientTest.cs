using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ReittiopasLib.Model;

namespace ReittiopasLib.Store.Tests
{
    [TestClass]
    public class ReittiopasClientTest
    {
        [TestMethod]
        public async Task Geocode()
        {
            var mockResponse = await TestUtils.GetMockResponse("GeocodeResponse_pisanmaki.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient("", "", mockHandler);
            IEnumerable<Place> places = await target.Geocode("Pisanmäki");
            places.Should().HaveCount(3);
            var first = places.First();
            first.Type.Should().Be(PlaceType.Address);
            first.Coords.Latitude.Should().Be(24.680782737009);
            first.Coords.Longitude.Should().Be(60.172288978365);

            var third = places.Last();
            third.Details.Lines.Should().HaveCount(3);

            var firstLine = third.Details.Lines.First();
            firstLine.Number.Should().Be("46");
            firstLine.Destination.Should().Be("Espoon keskus");

            var lastLine = third.Details.Lines.Last();
            lastLine.Number.Should().Be("195N");
            lastLine.Destination.Should().Be("Elielinaukio, l. 36");
        }

        [TestMethod]
        public async Task ReverseGeocode_Address()
        {
            var mockResponse = await TestUtils.GetMockResponse("ReverseGeocode_Pisanmaki_Address.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient("", "", mockHandler);
            var coords = new Coordinates(24.68024810338, 60.171699323538);
            IEnumerable<Place> places = await target.ReverseGeocode(coords);
            places.Should().HaveCount(3);
            places.Should().NotContain(p => p.Type != PlaceType.Address);
        }

        [TestMethod]
        public async Task ReverseGeocode_POI()
        {
            var mockResponse = await TestUtils.GetMockResponse("ReverseGeocode_Pisanmaki_POI.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient("", "", mockHandler);
            var coords = new Coordinates(24.68024810338, 60.171699323538);
            IEnumerable<Place> places = await target.ReverseGeocode(coords, PlaceKind.POI);
            places.Should().HaveCount(3);
            places.Should().NotContain(p => p.Type != PlaceType.POI);
        }

        [TestMethod]
        public async Task ReverseGeocode_Stop()
        {
            var mockResponse = await TestUtils.GetMockResponse("ReverseGeocode_Pisanmaki_Stop.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient("", "", mockHandler);
            var coords = new Coordinates(24.68024810338, 60.171699323538);
            IEnumerable<Place> places = await target.ReverseGeocode(coords, PlaceKind.Stop);
            places.Should().HaveCount(3);
            places.Should().NotContain(p => p.Type != PlaceType.Stop);
        }

        [TestMethod]
        public async Task FindRoute_Stop()
        {
            var mockResponse = await TestUtils.GetMockResponse("Route_Klaneettitie_Liisankatu.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient("", "", mockHandler);
            var request = new RouteParameters()
                {
                    From = new Coordinates(24.876620087474, 60.237461895493),
                    To = new Coordinates(24.959322353265, 60.174058109457)
                };
            IEnumerable<Route> routes = await target.FindRoute(request);
            routes.Should().HaveCount(3);

            var firstRoute = routes.ElementAt(0);
            firstRoute.Duration.TotalSeconds.Should().Be(2040);

            var firstLeg = firstRoute.Legs.First();
            var legStart = firstLeg.Locations.First();
            legStart.Coord.ShouldBeEquivalentTo(request.From);
        }
    }
}
