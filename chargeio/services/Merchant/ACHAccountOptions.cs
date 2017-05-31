using System;
using Newtonsoft.Json;

namespace ChargeIo.Services.Merchant
{
    [Serializable]
    public class ACHAccountOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("required_payment_fields")]
        public string RequiredPaymentFields { get; set; }
    }
}
