using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace chargeio
{
    public class SavedPaymentFromTokenOptions
    {
        [JsonProperty("token_id")]
        public string TokenId { get; set; }
    }
}
