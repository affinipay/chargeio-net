using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chargeio
{
    [Serializable]
    public class CreditOptions
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("method")]
        public object Method { get; set; }
    }
}
