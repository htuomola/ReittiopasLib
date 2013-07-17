using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ReittiopasLib
{
    public class HslDateTimeConverter : IsoDateTimeConverter
    {
        public HslDateTimeConverter()
        {
            DateTimeFormat = "yyyyMMddHHmm";
            Culture = CultureInfo.InvariantCulture;
            
        }
    }
}