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

        public virtual MerchantAccount UpdateMerchantAccount(string accountId, MerchantAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.MerchantAccounts, accountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), AuthUser, AuthPassword);
            return Mapper<MerchantAccount>.MapFromJson(response);
        }

        public virtual ACHAccount UpdateACHAccount(string achAccountId, ACHAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.ACHAccounts, achAccountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), AuthUser, AuthPassword);
            return Mapper<ACHAccount>.MapFromJson(response);
        }
    }
}