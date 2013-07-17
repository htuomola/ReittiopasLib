using System;
using System.Collections.Generic;

namespace ReittiopasLib.Model
{
    public class Location
    {
        public Coordinates Coord { get; set; }

        public DateTime ArrivalTime { get; set; }

        public DateTime DepartureTime { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ShortCode { get; set; }

        public string StopAddress { get; set; }
    }

    public class Leg
    {
        public int Length { get; set; }

        public TimeSpan Duration { get; set; }

        public string Type { get; set; }

        public IEnumerable<Location> Locations { get; set; }

        public string Code { get; set; }
    }

    public class Route
    {
        public double Length { get; set; }

        public TimeSpan Duration { get; set; }

        public IEnumerable<Leg> Legs { get; set; }
    }

}
