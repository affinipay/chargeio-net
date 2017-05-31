using System;
using ChargeIo.Infrastructure;
using Newtonsoft.Json;

namespace ChargeIo.Models
{
    [Serializable]
    public class Credit : Transaction
    {
        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public object PaymentMethod { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("void_reference")]
        public string VoidReference { get; set; }
    }
}
