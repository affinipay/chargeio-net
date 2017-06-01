using System;
using ChargeIo.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIo.Models
{
    [Serializable]
    public class RecurringCharge
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_reason")]
        public string StatusReason { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public object PaymentMethod { get; set; }

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

        [JsonProperty("total_occurrences")]
        public int? TotalOccurrences { get; set; }

        [JsonProperty("total_amount")]
        public int? TotalAmount { get; set; }

        [JsonProperty("next_payment")]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime NextPayment { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }
    }
}
