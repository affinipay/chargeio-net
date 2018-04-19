using System;
using System.Collections.Generic;
using ChargeIO.Infrastructure;
using Newtonsoft.Json;

namespace ChargeIO.Models
{
    [Serializable]
    public class RecurringChargeOccurrence
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("recurring_charge_id")]
        public string RecurringChargeId { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("due_date")]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime DueDate { get; set; }

        [JsonProperty("attempts")]
        public int? Attempts { get; set; }

        [JsonProperty("last_attempt")]
        public DateTime LastAttempt { get; set; }

        [JsonProperty("transactions")]
        public List<Charge> Charges { get; set; }
    }
}
