namespace ChargeIo.Infrastructure
{
	internal static class Urls
	{
```		private static string BaseUrl => Configuration.ApiUrl;

	    public static string MerchantAccounts => BaseUrl + "/accounts";

	    public static string AchAccounts => BaseUrl + "/ach-accounts";

	    public static string Banks => BaseUrl + "/banks";

	    public static string Cards => BaseUrl + "/cards";

	    public static string Merchant => BaseUrl + "/merchant";

	    public static string Transactions => BaseUrl + "/transactions";

	    public static string Charges => BaseUrl + "/charges";

	    public static string Holds => BaseUrl + "/charges/holds";

	    public static string Refunds => BaseUrl + "/refunds";

		public static string Tokens => BaseUrl + "/tokens";

		public static string Credits => BaseUrl + "/credits";

		public static string RecurringCharges => BaseUrl + "/recurring/charges";

		public static string Signatures => BaseUrl + "/signatures";
	}
}
