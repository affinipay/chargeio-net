using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace chargeio
{
    [Serializable]
    public class Schedule
    {
        [JsonProperty("interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty("interval_delay")]
        public int? IntervalDelay { get; set; }

        [JsonProperty("start")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? End { get; set; }

        [JsonProperty("days")]
        public List<String> Days { get; set; }
    }
}
