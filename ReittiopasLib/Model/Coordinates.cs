namespace ReittiopasLib.Model
{
    public struct Coordinates
    {
        public readonly double Latitude;
        public readonly double Longitude;

        public Coordinates(double longitude, double latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Get coordinates' string representation in the format expected by Reittiopas API
        /// </summary>
        /// <returns>This coordinate values in "latitude,longitude" format </returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }
    }
}