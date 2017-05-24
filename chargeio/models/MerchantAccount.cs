using System;
using Newtonsoft.Json;

namespace chargeio
{
    [Serializable]
    public class MerchantAccount
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

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("trust_account")]
        public bool? TrustAccount { get; set; }

        [JsonProperty("cvv_policy")]
        public string CVVPolicy { get; set; }

        [JsonProperty("avs_policy")]
        public string AVSPolicy { get; set; }

        [JsonProperty("ignore_avs_failure_if_cvv_match")]
        public bool IgnoreAVSFailureIfCVVMatch { get; set; }

        [JsonProperty("required_payment_fields")]
        public string RequiredPaymentFields { get; set; }

        [JsonProperty("swipe_cvv_policy")]
        public string SwipeCVVPolicy { get; set; }

        [JsonProperty("swipe_avs_policy")]
        public string SwipeAVSPolicy { get; set; }

        [JsonProperty("swipe_ignore_avs_failure_if_cvv_match")]
        public bool SwipeIgnoreAVSFailureIfCVVMatch { get; set; }

        [JsonProperty("swipe_required_payment_fields")]
        public string SwipeRequiredPaymentFields { get; set; }

        [JsonProperty("transaction_allowed_countries")]
        public string TransactionAllowedCountries { get; set; }

        [JsonProperty("accepted_card_types")]
        public String AcceptedCardTypes { get; set; }
    }
}
