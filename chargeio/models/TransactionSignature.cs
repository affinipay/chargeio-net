using System;
using Newtonsoft.Json;

namespace ChargeIo.Models
{
    [Serializable]
    public class TransactionSignature
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
