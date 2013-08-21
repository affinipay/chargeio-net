namespace ChargeIO
{
	internal static class Urls
	{
        private static string BaseUrl
        {
            get { return Configuration.GetApiUrl(); }
        }
        public static string Accounts
        {
            get { return BaseUrl + "/accounts"; }
        }
        public static string BankAccounts
        {
            get { return BaseUrl + "/bank-accounts"; }
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
        public static string Refunds
        {
            get { return BaseUrl + "/refunds"; }
        }
        public static string Tokens
        {
            get { return BaseUrl + "/tokens"; }
        }
        public static string Transfers
        {
            get { return BaseUrl + "/transfers"; }
        }
	}
}
