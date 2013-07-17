using System.Collections.Generic;

namespace ReittiopasLib.Model
{
    public class PlaceDetails
    {
        public int HouseNumber { get; set; }

        public string Address { get; set; }

        public string Code { get; set; }

        public string ShortCode { get; set; }

        public double? ChangeCost { get; set; }

        public IEnumerable<LineInfo> Lines { get; set; }

        //public int? TransportTypeId { get; set; }
        //public TransportType? TransportType { get; set; }

        public string PoiType { get; set; }
    }

    public class LineInfo
    {
        public string Number { get; set; }

        public string Destination { get; set; }
    }

    //public enum TransportType
    //{

    //}

}