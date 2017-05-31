using System;
using Newtonsoft.Json;

namespace ChargeIo.Services.Transactions
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
