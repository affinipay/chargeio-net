namespace ChargeIO
{
	public class MerchantService
	{
		private string AuthUser { get; set; }
        private string AuthPassword { get; set; }

        public MerchantService(string authUser = null, string authPassword = null)
		{
			AuthUser = authUser;
            AuthPassword = authPassword;
		}

        public virtual Merchant GetMerchant()
		{
			var response = Requestor.GetString(Urls.Merchant, AuthUser, AuthPassword);

            return Mapper<Merchant>.MapFromJson(response);
		}
    
        public virtual Merchant UpdateMerchant(MerchantOptions m)
        {
            var merchant = ParameterBuilder.BuildJsonPostParameters(m);
            var response = Requestor.PutJson(Urls.Merchant, merchant, AuthUser, AuthPassword);

            return Mapper<Merchant>.MapFromJson(response);
        }

        public virtual Account UpdateAccount(string accountId, AccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.Accounts, accountId);

            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), AuthUser, AuthPassword);

            return Mapper<Account>.MapFromJson(response);
        }

        public virtual BankAccount UpdateBankAccount(string bankAccountId, BankAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.BankAccounts, bankAccountId);

            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), AuthUser, AuthPassword);

            return Mapper<BankAccount>.MapFromJson(response);
        }

    }
}