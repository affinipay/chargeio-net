using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class Schedule
    {
        [JsonProperty("interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty("interval_delay")]
        public int? IntervalDelay { get; set; }

        [JsonProperty("start")]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? End { get; set; }

        [JsonProperty("days")]
        public List<String> Days { get; set; }
    }
}
