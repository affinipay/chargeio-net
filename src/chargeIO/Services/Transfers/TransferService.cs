using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ChargeIO
{
    public class TransferService
    {
        private string AuthUser { get; set; }
        private string AuthPassword { get; set; }

        public TransferService(string authUser = null, string authPassword = null)
        {
            AuthUser = authUser;
            AuthPassword = authPassword;
        }

        public virtual Transfer Create(TransferOptions options)
        {
            var response = Requestor.PostJson(
                Urls.Transfers,
                ParameterBuilder.BuildJsonPostParameters(options),
                AuthUser,
                AuthPassword);

            return Mapper<Transfer>.MapFromJson(response);
        }

        public virtual Transfer Cancel(string transferId)
        {
            var url = string.Format("{0}/{1}/cancel", Urls.Transfers, transferId);

            var response = Requestor.PostJson(url, null, AuthUser, AuthPassword);

            return Mapper<Transfer>.MapFromJson(response);
        }

        public virtual Transfer Get(string transferId)
        {
            var url = string.Format("{0}/{1}", Urls.Transfers, transferId);

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Transfer>.MapFromJson(response);
        }

        public virtual SearchResults<Transfer> List(int page = 1, int page_size = 20, string accountId = null)
        {
            var url = Urls.Transfers;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", page_size.ToString());

            if (!string.IsNullOrEmpty(accountId))
                url = ParameterBuilder.ApplyParameterToUrl(url, "account_id", accountId);

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Transfer>.MapCollectionFromJson(response);
        }

        
    }
}
