using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChargeIo.Models
{
    [Serializable]
    public class Merchant
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact_name")]
        public string ContactName { get; set; }

        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }

        [JsonProperty("contact_phone")]
        public string ContactPhone { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("merchant_accounts")]
        public List<MerchantAccount> MerchantAccounts { get; set; }

        [JsonProperty("ach_accounts")]
        public List<AchAccount> AchAccounts { get; set; }

        [JsonProperty("api_allowed_ip_address_ranges")]
        public string ApiAllowedIpAddressRanges { get; set; }

    }
}
