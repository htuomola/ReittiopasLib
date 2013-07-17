﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using Newtonsoft.Json;

namespace ReittiopasLib.Dto
{

    public class Details
    {

        [JsonProperty("houseNumber")]
        public int HouseNumber { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("shortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("changeCost")]
        public double? ChangeCost { get; set; }

        [JsonProperty("lines")]
        public string[] Lines { get; set; }

        [JsonProperty("transport_type_id")]
        public int? TransportTypeId { get; set; }

        [JsonProperty("poiType")]
        public string PoiType { get; set; }
    }

    public class PlaceDto
    {

        [JsonProperty("locType")]
        public string LocType { get; set; }

        [JsonProperty("locTypeId")]
        public int LocTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("matchedName")]
        public string MatchedName { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("coords")]
        public string Coords { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }
    }

}
