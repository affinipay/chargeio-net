using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIO
{
    public class Charge
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("failure_code")]
        public string FailureCode { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        [JsonProperty("auto_capture")]
        public bool AutoCapture { get; set; }

        [JsonProperty("amount_refunded")]
        public int? AmountInCentsRefunded { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("capture_reference")]
        public string CaptureReference { get; set; }

        [JsonProperty("void_reference")]
        public string VoidReference { get; set; }

        [JsonProperty("refunds")]
        public IList<Refund> Refunds { get; set; }
    }
}
