using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIO
{
    public class RecurringChargeOptions
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("method")]
        public IPaymentMethod Method { get; set; }

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
