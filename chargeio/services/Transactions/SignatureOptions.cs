using System;
using Newtonsoft.Json;

namespace ChargeIo.Services.Transactions
{
    [Serializable]
    public class SignatureOptions
    {
        [JsonProperty("gratuity")]
        public int? GratuityInCents { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
