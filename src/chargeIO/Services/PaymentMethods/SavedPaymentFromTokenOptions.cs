using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class SavedPaymentFromTokenOptions : IPaymentMethod
    {
        [JsonProperty("token_id")]
        public string TokenId { get; set; }
    }
}
