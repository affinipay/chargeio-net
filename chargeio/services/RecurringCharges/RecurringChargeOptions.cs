using System;
using ChargeIO.Models;
using Newtonsoft.Json;

namespace ChargeIO.Services.RecurringCharges
{
    [Serializable]
    public class RecurringChargeOptions
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("method")]
        public object Method { get; set; }

        [JsonProperty("schedule")]
        public Schedule Schedule { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
        
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("max_occurrences")]
        public int? MaxOccurrences { get; set; }

        [JsonProperty("max_amount")]
        public int? MaxAmount { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
