using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class MerchantAccountOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("required_payment_fields")]
        public string RequiredPaymentFields { get; set; }

        [JsonProperty("cvv_policy")]
        public string CVVPolicy { get; set; }

        [JsonProperty("avs_policy")]
        public string AVSPolicy { get; set; }

        [JsonProperty("ignore_avs_failure_if_cvv_match")]
        public bool IgnoreAVSFailureIfCVVMatch { get; set; }
    }
}
