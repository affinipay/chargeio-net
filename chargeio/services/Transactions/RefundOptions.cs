using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chargeio
{
    [Serializable]
    public class RefundOptions
    {
        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
