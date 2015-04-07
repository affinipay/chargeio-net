using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    [JsonConverter(typeof(CardReferenceConverter))]
    public class CardReferenceOptions : IPaymentMethod
    {
        public string CardId { get; set; }
    }
}
