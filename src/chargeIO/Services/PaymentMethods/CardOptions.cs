using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ChargeIO
{
    public class CardOptions : IPaymentMethod
    {
        [JsonProperty("type")]
        public string Type { get { return "card"; } }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("cvv")]
        public string Cvv { get; set; }

        [JsonProperty("exp_month")]
        public int? ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public int? ExpYear { get; set; }

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
