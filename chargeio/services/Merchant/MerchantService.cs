using ChargeIo.services;
using ChargeIO.Infrastructure;
using ChargeIO.Models;

namespace ChargeIO.Services.Merchant
{
    public sealed class MerchantService : ServiceBase
    {
        public MerchantService(string secretKey = null) : base(secretKey)
        {
        }

        public Models.Merchant GetMerchant()
        {
            var response = Requestor.GetString(Urls.Merchant, SecretKey);
            return Mapper<Models.Merchant>.MapFromJson(response);
        }

        public Models.Merchant UpdateMerchant(MerchantOptions m)
        {
            var merchant = ParameterBuilder.BuildJsonPostParameters(m);
            var response = Requestor.PutJson(Urls.Merchant, merchant, SecretKey);

            return Mapper<Models.Merchant>.MapFromJson(response);
        }

        public MerchantAccount UpdateMerchantAccount(string accountId, MerchantAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.MerchantAccounts, accountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), SecretKey);
            return Mapper<MerchantAccount>.MapFromJson(response);
        }

        public AchAccount UpdateAchAccount(string achAccountId, AchAccountOptions o)
        {
            var url = string.Format("{0}/{1}", Urls.AchAccounts, achAccountId);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(o), SecretKey);
            return Mapper<AchAccount>.MapFromJson(response);
        }
    }
}