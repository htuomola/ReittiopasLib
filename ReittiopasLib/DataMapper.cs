using System;
using System.Linq;
using ReittiopasLib.Dto;
using ReittiopasLib.Model;

namespace ReittiopasLib
{
    public class DataMapper
    {
        public static Place MapPlace(PlaceDto placeDto)
        {
            return new Place()
                {
                    City = placeDto.City,
                    Details = MapDetails(placeDto.Details),
                    MatchedName = placeDto.MatchedName,
                    Name = placeDto.Name,
                    Type = MapPlaceType(placeDto.LocType),
                    Coords = ParseCoordinates(placeDto.Coords)
                };
        }

        private static PlaceDetails MapDetails(Details details)
        {
            return new PlaceDetails()
                {
                    Address = details.Address,
                    ChangeCost = details.ChangeCost,
                    Code = details.Code,
                    HouseNumber = details.HouseNumber,
                    Lines = details.Lines != null ? details.Lines.Select(ParseLine) : null,
                    ShortCode = details.ShortCode,
                    PoiType = details.PoiType
                };
        }

        private static PlaceType MapPlaceType(string locType)
        {
            switch (locType)
            {
                case "address":
                    return PlaceType.Address;
                case "poi":
                    return PlaceType.POI;
                case "street":
                    return PlaceType.Street;
                case "stop":
                    return PlaceType.Stop;
                default:
                    throw new ArgumentException("Unknown value: " + locType, "locType");
            }
        }

        public static Route MapRoute(RouteDto routeDto)
        {
            return new Route
                {
                    Duration = ParseDuration(routeDto.Duration),
                    Legs = routeDto.Legs.Select(MapLeg),
                    Length = routeDto.Length
                };
        }

        private static Leg MapLeg(LegDto dto)
        {
            return new Leg
                {
                    Code = dto.Code,
                    Duration = ParseDuration(dto.Duration),
                    Length = dto.Length,
                    Locations = dto.Locs.Select(MapLoc),
                    Type = dto.Type
                };
        }

        private static Location MapLoc(Loc dto)
        {
            return new Location
                {
                    Name = dto.Name,
                    ShortCode = dto.ShortCode,
                    StopAddress = dto.StopAddress,
                    Code = dto.Code,
                    Coord = MapCoordinates(dto.Coord),
                    ArrivalTime = dto.ArrivalTime,
                    DepartureTime = dto.DepartureTime
                };
        }

        private static Coordinates MapCoordinates(Coord coords)
        {
            return new Coordinates(coords.Longitude, coords.Latitude);
        }

        private static LineInfo ParseLine(string lineString)
        {
            // format e.g.  "2046  2:Hyljelahti",

            var splitIndex = lineString.IndexOf(':');
            var code = lineString.Substring(1, 5).Trim(new[]{'0', ' '});
            var destination = lineString.Substring(splitIndex + 1);

            return new LineInfo() {Number = code, Destination = destination};
        }

        private static Coordinates ParseCoordinates(string coords)
        {
            var parts = coords.Split(',');
            var latitude = double.Parse(parts[0]);
            var longitude = double.Parse(parts[1]);
            return new Coordinates(longitude, latitude);
        }

        private static TimeSpan ParseDuration(double duration)
        {
            return TimeSpan.FromSeconds(duration);
        }
    }
}