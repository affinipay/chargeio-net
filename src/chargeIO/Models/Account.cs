using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace ChargeIO
{
    public class Account
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("required_card_fields")]
        public string RequiredCardFields { get; set; }

        [JsonProperty("cvv_policy")]
        public string CVVPolicy { get; set; }

        [JsonProperty("avs_policy")]
        public string AVSPolicy { get; set; }

        [JsonProperty("ignore_avs_failure_if_cvv_match")]
        public bool IgnoreAVSFailureIfCVVMatch { get; set; }

        [JsonProperty("transaction_allowed_countries")]
        public string TransactionAllowedCountries { get; set; }
    }
}
