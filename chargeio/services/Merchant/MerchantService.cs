using ChargeIo.Infrastructure;
using ChargeIo.Models;

namespace ChargeIo.Services.Merchant
{
	public class MerchantService
	{
        private string SecretKey { get; set; }
        
        public MerchantService(string secretKey = null)
		{
            SecretKey = secretKey ?? Configuration.SecretKey;
		}

        public virtual Models.Merchant GetMerchant()
		{
			var response = Requestor.GetString(Urls.Merchant, SecretKey);
            return Mapper<Models.Merchant>.MapFromJson(response);
		}
    
        public virtual Models.Merchant UpdateMerchant(MerchantOptions m)
        {
            var merchant = ParameterBuilder.BuildJsonPostParameters(m);
            var response = Requestor.PutJson(Urls.Merchant, merchant, SecretKey);

            return Mapper<Models.Merchant>.MapFromJson(response);
        }

        public virtual MerchantAccount UpdateMerchantAccount(string accountId, MerchantAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.MerchantAccounts, accountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), SecretKey);
            return Mapper<MerchantAccount>.MapFromJson(response);
        }

        public virtual AchAccount UpdateAchAccount(string achAccountId, AchAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.AchAccounts, achAccountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), SecretKey);
            return Mapper<AchAccount>.MapFromJson(response);
        }
    }
}