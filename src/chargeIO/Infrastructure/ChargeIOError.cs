using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChargeIO
{
	public class ChargeIOError
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("level")]
		public string Level { get; set; }

		[JsonProperty("context")]
		public string Context { get; set; }

    	[JsonProperty("sub_code")]
		public string SubCode { get; set; }

        public string Description { 
            get {
                try
                {
                    string key = string.Format("{0} {1} {2} {3}", Level, Code, Context, SubCode);
                    return DEFAULT_RESPONSE_MAPPINGS[key];
                }
                catch (KeyNotFoundException)
                {
                    try
                    {
                        string key = string.Format("{0} {1} {2}", Level, Code, Context);
                        return DEFAULT_RESPONSE_MAPPINGS[key];
                    }
                    catch (KeyNotFoundException)
                    {
                        try
                        {
                            string key = string.Format("{0} {1}", Level, Code);
                            return DEFAULT_RESPONSE_MAPPINGS[key];
                        }
                        catch (KeyNotFoundException)
                        {
                            return string.Format("Unknown ChargeIO {0}", Level);
                        }
                    }
                }
            } 
        }

        private static readonly Dictionary<string, string> DEFAULT_RESPONSE_MAPPINGS = new Dictionary<string, string>
        {
            {"error not_authorized"                    , "Not authorized"},
            // General operation responses
            { "error timeout"                          , "The operation timed out." },
            { "error server_error"                     , "An unexpected error occurred. Please contact support." },
            { "error card_processor_not_available"     , "Unable to process the payment at this time. Please try again later." },
            { "error card_processing_error"            , "An unexpected error occurred processing the payment. Please contact support." },
            { "error unavailable_for_merchant_mode"    , "The operation cannot be completed using the configured merchant key." },
            { "error unavailable_for_merchant_policy"  , "The operation is unavailable due to the current merchant policy." },
            { "error not_valid_for_transaction_status" , "The operation cannot be completed in the current status." },
            { "error not_valid_for_auto_capture"       , "The operation is unavailable for the transaction." },
            { "error search_failed"                    , "The search could not be processed as defined." },

            { "error resource_not_found"                               , "Resource Not Found." },

            // General validation responses
            { "error invalid_data"                                        , "The value is invalid." },

            { "error invalid_data currency"                               , "Invalid Currency." },

            { "error invalid_data name not_blank"                         , "Name cannot be blank." },
            { "error invalid_data name invalid_length"                    , "Name length is invalid." },
            { "error invalid_data amount below_minimum_value"             , "Amount is less than the minimum value." },
            { "error invalid_data amount invalid"                         , "Amount is invalid." },

            { "error invalid_data cvv_policy not_null"                      , "The CVV policy must be specified." },
            { "error invalid_data cvv_policy invalid"                       , "The CVV policy is invalid." },
            { "error invalid_data avs_policy not_null"                      , "The AVS policy must be specified." },
            { "error invalid_data avs_policy invalid"                       , "The AVS policy is invalid." },
            { "error invalid_data ignore_avs_failure_if_cvv_match invalid"  , "The setting for ignoring AVS failures when CVV matches is invalid." },
            { "error invalid_data transaction_allowed_countries invalid"    , "The transaction countries list is invalid." },
            { "error invalid_data max_daily_transactions invalid"           , "The maximum daily transactions policy is invalid." },
            { "error invalid_data max_daily_transaction_value invalid"      , "The maximum daily transaction value policy is invalid." },
            { "error invalid_data max_monthly_transactions invalid"         , "The maximum monthly transactions policy is invalid." },
            { "error invalid_data max_monthly_transaction_value invalid"    , "The maximum monthly transaction value policy is invalid." },
            { "error invalid_data api_allowed_ip_address_ranges invalid"    , "The API IP address ranges list is invalid." },

            // Management operation responses
            { "error cannot_delete_primary_account"             , "The primary account cannot be deleted." },
            { "error account_transaction_countries_too_lenient" , "The account transaction allowed countries contains one or more countries not permitted by the tenant." },

            // Card validation and processing responses
            { "error invalid_data card.card_number"                            , "Card number is invalid." },
            { "error invalid_data card.card_number not_null"                   , "Card number cannot be blank." },
            { "error invalid_data card.card_number not_blank"                  , "Card number cannot be blank." },
            { "error invalid_data card.card_number invalid_length"             , "Card number length is invalid." },
            { "error invalid_data card.card_number invalid"                    , "Card number is invalid." },
            { "error invalid_data card.card_exp_month"                         , "Expiration month is invalid." },
            { "error invalid_data card.card_exp_month not_null"                , "Expiration month cannot be blank." },
            { "error invalid_data card.card_exp_month below_minimum_value"     , "Expiration month is invalid." },
            { "error invalid_data card.card_exp_month exceeds_maximum_value"   , "Expiration month is invalid." },
            { "error invalid_data card.card_exp_month invalid"                 , "Expiration month is invalid." },
            { "error invalid_data card.card_exp_year"                          , "Expiration year is invalid." },
            { "error invalid_data card.card_exp_year not_null"                 , "Expiration year cannot be blank." },
            { "error invalid_data card.card_exp_year below_minimum_value"      , "Expiration year is invalid." },
            { "error invalid_data card.card_exp_year number_out_of_range"      , "Expiration year is invalid." },
            { "error invalid_data card.card_exp_year invalid"                  , "Expiration year is invalid." },
            { "error invalid_data card.card_cvv"                               , "Card CVV is invalid." },
            { "error invalid_data card.card_cvv not_null"                      , "Card CVV cannot be blank." },
            { "error invalid_data card.card_cvv not_blank"                     , "Card CVV cannot be blank." },
            { "error invalid_data card.card_cvv invalid_length"                , "Card CVV length is invalid." },
            { "error invalid_data card.card_cvv invalid"                       , "Card CVV is invalid." },
            { "error invalid_data card.name"                                   , "Card name is invalid." },
            { "error invalid_data card.name not_null"                          , "Card name cannot be blank." },
            { "error invalid_data card.name not_blank"                         , "Card name cannot be blank." },
            { "error invalid_data card.name invalid_length"                    , "Card name length is invalid." },
            { "error invalid_data card.address1"                               , "Card address1 is invalid." },
            { "error invalid_data card.address1 not_null"                      , "Card address1 cannot be blank." },
            { "error invalid_data card.address1 not_blank"                     , "Card address1 cannot be blank." },
            { "error invalid_data card.address1 invalid_length"                , "Card address1 length is invalid." },
            { "error invalid_data card.address2"                               , "Card address2 is invalid." },
            { "error invalid_data card.address2 not_null"                      , "Card address2 cannot be blank." },
            { "error invalid_data card.address2 not_blank"                     , "Card address2 cannot be blank." },
            { "error invalid_data card.address2 invalid_length"                , "Card address2 length is invalid." },
            { "error invalid_data card.city"                                   , "Card city is invalid." },
            { "error invalid_data card.city not_null"                          , "Card city cannot be blank." },
            { "error invalid_data card.city not_blank"                         , "Card city cannot be blank." },
            { "error invalid_data card.city invalid_length"                    , "Card city length is invalid." },
            { "error invalid_data card.state"                                  , "Card state is invalid." },
            { "error invalid_data card.state not_null"                         , "Card state cannot be blank." },
            { "error invalid_data card.state not_blank"                        , "Card state cannot be blank." },
            { "error invalid_data card.state invalid_length"                   , "Card state length is invalid." },
            { "error invalid_data card.state invalid"                          , "Card state is invalid." },
            { "error invalid_data card.postal_code"                            , "Card postal code is invalid." },
            { "error invalid_data card.postal_code not_null"                   , "Card postal code cannot be blank." },
            { "error invalid_data card.postal_code not_blank"                  , "Card postal code cannot be blank." },
            { "error invalid_data card.postal_code invalid_length"             , "Card postal code length is invalid." },
            { "error invalid_data card.postal_code invalid"                    , "Card postal code is invalid." },
            { "error invalid_data card.country"                                , "Card country is invalid." },
            { "error invalid_data card.country not_null"                       , "Card country cannot be blank." },
            { "error invalid_data card.country not_blank"                      , "Card country cannot be blank." },
            { "error invalid_data card.country invalid_length"                 , "Card country length is invalid." },
            { "error invalid_data card.country invalid"                        , "Card country is invalid." },

            { "error card_number_invalid"                    , "The card number is invalid." },
            { "error card_expired"                           , "The card is expired." },
            { "error refund_exceeds_transaction"             , "Amount of refund exceeds remaining transaction balance." },
            { "error exceeds_authorized_amount"              , "The amount to capture exceeds the authorized amount." },
            { "error currency_mismatch"                      , "The specified currency does not match the transaction''s currency." },
            { "error card_cvv_not_provided"                  , "Card CVV must be provided." },
            { "error card_cvv_incorrect"                     , "Card CVV is incorrect." },
            { "error card_declined"                          , "The card was declined." },
            { "error card_declined_processing_error"         , "The card was declined due to a processing error." },
            { "error card_declined_hold"                     , "The card was declined - hold card." },
            { "error card_type_not_accepted"                 , "The card type is not accepted." },
            { "error card_avs_rejected"                      , "Address verification failed." },
            { "error card_declined_insufficient_funds"       , "The card was declined due to insufficient funds." },
            { "error card_declined_limit_exceeded"           , "The card was declined due to limits exceeded." },
            { "error merchant_trans_max_amount_exceeded"     , "Merchant transaction maximum amount exceeded." },
            { "error merchant_trans_daily_count_exceeded"    , "Merchant transaction daily limit exceeded." },
            { "error merchant_trans_daily_amount_exceeded"   , "Merchant transaction daily amount exceeded." },
            { "error merchant_trans_monthly_count_exceeded"  , "Merchant transaction monthly limit exceeded." },
            { "error merchant_trans_monthly_amount_exceeded" , "Merchant transaction monthly amount exceeded." },

            // ACH processing responses
            { "error invalid_data bank.bank_routing_number"                , "Routing number is invalid." },
            { "error invalid_data bank.bank_routing_number not_null"       , "Routing number cannot be blank." },
            { "error invalid_data bank.bank_routing_number not_blank"      , "Routing number cannot be blank." },
            { "error invalid_data bank.bank_routing_number invalid_length" , "Routing number length is invalid." },
            { "error invalid_data bank.bank_routing_number invalid"        , "Routing number is invalid." },
            { "error invalid_data bank.bank_account_number"                , "Account number is invalid." },
            { "error invalid_data bank.bank_account_number not_null"       , "Account number cannot be blank." },
            { "error invalid_data bank.bank_account_number not_blank"      , "Account number cannot be blank." },
            { "error invalid_data bank.bank_account_number invalid_length" , "Account number length is invalid." },
            { "error invalid_data bank.bank_account_number invalid"        , "Account number is invalid." },
            { "error invalid_data bank.bank_account_type"                  , "Account type is invalid." },
            { "error invalid_data bank.bank_account_type not_null"         , "Account type cannot be blank." },

            { "error ach_declined"                           , "The transfer was declined." },
            { "error ach_declined_hold"                      , "The transfer was declined due to possible fraud or theft alert." },
            { "error ach_declined_duplicate"                 , "The transfer was declined as a duplicate." },
            { "error ach_invalid_account_number"             , "The account number is invalid." },
            { "error ach_invalid_routing_number"             , "The routing number is invalid." },
            { "error ach_insufficient_funds"                 , "The transfer was declined due to insufficient funds." },
            { "error ach_account_not_found"                  , "The account was not found." },
            { "error ach_account_closed"                     , "The account is closed." },
            { "error ach_account_frozen"                     , "The account is frozen." },
            { "error ach_limit_exceeded"                     , "The transfer was declined due to limits exceeded." },
            { "error ach_invalid_merchant_configuration"     , "The transfer could not be completed due to invalid merchant ACH configuration." },
            { "error ach_processor_not_available"            , "The transfer could not be completed due to a communication problem." },
            { "error ach_processing_error"                   , "The transfer was declined due to a processing error." }
        };
	}
}