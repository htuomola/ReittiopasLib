namespace ReittiopasLib.Model
{
    public class Place
    {
        public PlaceType Type { get; set; }

        public string Name { get; set; }

        public string MatchedName { get; set; }

        //public string Lang { get; set; }

        public string City { get; set; }

        public Coordinates Coords { get; set; }

        public PlaceDetails Details { get; set; }
    }

    public enum PlaceType
    {
        Address,
        Stop,
        Street,
        POI
    }
}
