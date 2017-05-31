namespace ChargeIo.Infrastructure
{
	internal static class Urls
	{
        private static string BaseUrl
        {
            get { return Configuration.ApiUrl + Configuration.ApiVersion; }
        }
        public static string MerchantAccounts
        {
            get { return BaseUrl + "/accounts"; }
        }
        public static string ACHAccounts
        {
            get { return BaseUrl + "/ach-accounts"; }
        }
        public static string Banks
        {
            get { return BaseUrl + "/banks"; }
        }
        public static string Cards
        {
            get { return BaseUrl + "/cards"; }
        }
        public static string Merchant
		{
			get { return BaseUrl + "/merchant"; }
		}
        public static string Transactions
        {
            get { return BaseUrl + "/transactions"; }
        }
        public static string Charges
        {
            get { return BaseUrl + "/charges"; }
        }
        public static string Holds
        {
            get { return BaseUrl + "/charges/holds"; }
        }
        public static string Refunds
        {
            get { return BaseUrl + "/refunds"; }
        }
        public static string Tokens
        {
            get { return BaseUrl + "/tokens"; }
        }
        public static string Credits
        {
            get { return BaseUrl + "/credits"; }
        }
        public static string RecurringCharges
        {
            get { return BaseUrl + "/recurring/charges"; }
        }
        public static string Signatures
        {
            get { return BaseUrl + "/signatures"; }
        }
	}
}
