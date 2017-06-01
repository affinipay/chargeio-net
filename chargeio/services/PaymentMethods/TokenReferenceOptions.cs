using ChargeIo.Infrastructure;
using ChargeIO;
using Newtonsoft.Json;

namespace ChargeIo.Services.PaymentMethods
{
    [JsonConverter(typeof(TokenReferenceConverter))]
    public class TokenReferenceOptions : IPaymentMethod
    {
        public string TokenId { get; set; }
    }
}
