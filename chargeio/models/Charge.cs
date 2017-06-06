using System;
using System.Collections.Generic;
using ChargeIO.Infrastructure;
using Newtonsoft.Json;

namespace ChargeIO.Models
{
    [Serializable]
    public class Charge : Transaction
    {
        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("gratuity")]
        public int? GratuityInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("capture_reference")]
        public string CaptureReference { get; set; }

        [JsonProperty("void_reference")]
        public string VoidReference { get; set; }

        [JsonProperty("method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public IPaymentMethod PaymentMethod { get; set; }

        [JsonProperty("avs_result")]
        public string AvsResult { get; set; }

        [JsonProperty("cvv_result")]
        public string CvvResult { get; set; }

        [JsonProperty("recurring_charge_id")]
        public string RecurringChargeId { get; set; }

        [JsonProperty("recurring_charge_occurrence_id")]
        public string RecurringChargeOccurrenceId { get; set; }

        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("amount_refunded")]
        public int? AmountInCentsRefunded { get; set; }

        [JsonProperty("refunds")]
        public IList<Refund> Refunds { get; set; }
    }
}
