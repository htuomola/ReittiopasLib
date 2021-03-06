﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReittiopasLib.Dto
{

    public class Coord
    {

        [JsonProperty("x")]
        public double Longitude { get; set; }

        [JsonProperty("y")]
        public double Latitude { get; set; }
    }

    public class Loc
    {

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("arrTime")]
        //[JsonConverter(typeof(HslDateTimeConverter))]
        public DateTime ArrivalTime { get; set; }

        [JsonProperty("depTime")]
        public DateTime DepartureTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("shortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("stopAddress")]
        public string StopAddress { get; set; }
    }

    public class LegDto
    {

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("locs")]
        public Loc[] Locs { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class RouteDto
    {

        [JsonProperty("length")]
        public double Length { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("legs")]
        public LegDto[] Legs { get; set; }
    }

}
