using System;
using Newtonsoft.Json;

namespace ChargeIO.Services.Merchant
{
    [Serializable]
    public class AchAccountOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("required_payment_fields")]
        public string RequiredPaymentFields { get; set; }
    }
}
