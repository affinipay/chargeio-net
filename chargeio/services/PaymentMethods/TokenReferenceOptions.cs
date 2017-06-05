using ChargeIO.Infrastructure;
using ChargeIO.Models;
using Newtonsoft.Json;

namespace ChargeIO.Services.PaymentMethods
{
    [JsonConverter(typeof(TokenReferenceConverter))]
    public class TokenReferenceOptions : IPaymentMethod
    {
        public string TokenId { get; set; }
    }
}
