using Newtonsoft.Json;

namespace ChargeIo.Services.PaymentMethods
{
    public class SavedPaymentFromTokenOptions
    {
        [JsonProperty("token_id")]
        public string TokenId { get; set; }
    }
}
