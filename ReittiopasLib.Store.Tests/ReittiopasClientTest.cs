using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ReittiopasLib.Model;
using Windows.Storage;

namespace ReittiopasLib.Store.Tests
{
    [TestClass]
    public class ReittiopasClientTest
    {
        [TestMethod]
        public async Task GeocodeTest()
        {
            var mockResponse = await TestUtils.GetMockResponse("GeocodeResponse_pisanmaki.json");
            var mockHandler = new MockHttpMessageHandler(mockResponse);
            var target = new ReittiopasClient(mockHandler);
            IEnumerable<Place> places = await target.Geocode("Pisanmäki");
            places.Count().Should().Be(3);
        }
    }
}
