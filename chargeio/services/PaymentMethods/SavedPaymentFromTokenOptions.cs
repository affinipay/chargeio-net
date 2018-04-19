using Newtonsoft.Json;

namespace ChargeIO.Services.PaymentMethods
{
    public class SavedPaymentFromTokenOptions
    {
        [JsonProperty("token_id")]
        public string TokenId { get; set; }
    }
}
