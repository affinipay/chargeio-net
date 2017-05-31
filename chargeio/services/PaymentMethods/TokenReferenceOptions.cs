using ChargeIo.Infrastructure;
using Newtonsoft.Json;

namespace ChargeIo.Services.PaymentMethods
{
    [JsonConverter(typeof(TokenReferenceConverter))]
    public class TokenReferenceOptions
    {
        public string TokenId { get; set; }
    }
}
