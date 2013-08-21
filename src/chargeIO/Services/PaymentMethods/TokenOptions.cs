using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class TokenOptions
    {

        [JsonProperty("number")]
        public string CardNumber { get; set; }

        [JsonProperty("type")]
        public string CardType { get; set; }

        [JsonProperty("exp_month")]
        public int CardExpirationMonth { get; set; }

        [JsonProperty("exp_year")]
        public int CardExpirationYear { get; set; }

        [JsonProperty("cvv")]
        public string CardCvv { get; set; }

        [JsonProperty("routing_number")]
        public string BankRoutingNumber { get; set; }

        [JsonProperty("account_number")]
        public string BankAccountNumber { get; set; }

        [JsonProperty("account_type")]
        public string BankAccountType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

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
    }
    
}