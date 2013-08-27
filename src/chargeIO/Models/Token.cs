using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class Token
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("number")]
        public string CardNumber { get; set; }

        [JsonProperty("type")]
        public string CardType { get; set; }

        [JsonProperty("exp_month")]
        public int CardExpirationMonth { get; set; }

        [JsonProperty("exp_year")]
        public int CardExpirationYear { get; set; }

        [JsonProperty("cvv")]
        public int CardCvv { get; set; }

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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
