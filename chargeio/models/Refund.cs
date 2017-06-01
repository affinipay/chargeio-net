using System;
using ChargeIo.Infrastructure;
using ChargeIO;
using Newtonsoft.Json;

namespace ChargeIo.Models
{
    [Serializable]
    public class Refund : Transaction
    {
        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public IPaymentMethod PaymentMethod { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("void_reference")]
        public string VoidReference { get; set; }
    }
}
