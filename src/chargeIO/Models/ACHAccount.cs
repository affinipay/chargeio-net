using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace ChargeIO
{
    public class ACHAccount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("required_payment_fields")]
        public string RequiredPaymentFields { get; set; }

        [JsonProperty("transaction_allowed_countries")]
        public string TransactionAllowedCountries { get; set; }
    }
}
