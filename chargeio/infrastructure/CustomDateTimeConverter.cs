using Newtonsoft.Json.Converters;

namespace ChargeIo.Infrastructure
{
    internal class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}