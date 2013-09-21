using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    [JsonConverter(typeof(TokenReferenceConverter))]
    public class TokenReferenceOptions : IPaymentMethod
    {
        public string TokenId { get; set; }
    }
}
