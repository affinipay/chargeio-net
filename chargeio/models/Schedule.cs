using System;
using System.Collections.Generic;
using ChargeIo.Infrastructure;
using Newtonsoft.Json;

namespace ChargeIo.Models
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
