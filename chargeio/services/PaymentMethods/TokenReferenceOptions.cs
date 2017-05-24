using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace chargeio
{
    [JsonConverter(typeof(TokenReferenceConverter))]
    public class TokenReferenceOptions
    {
        public string TokenId { get; set; }
    }
}
