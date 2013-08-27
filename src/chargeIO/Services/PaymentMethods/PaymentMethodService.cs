using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace ChargeIO
{
	public class PaymentMethodService
	{
		private string AuthUser { get; set; }
        private string AuthPassword { get; set; }

        public PaymentMethodService(string authUser = null, string authPassword = null)
		{
			AuthUser = authUser;
            AuthPassword = authPassword;
		}

        public virtual Card CreateCard(CardOptions options)
        {
            var response = Requestor.PostJson(
                Urls.Cards,
                ParameterBuilder.BuildJsonPostParameters(options),
                AuthUser,
                AuthPassword);

            return Mapper<Card>.MapFromJson(response);
        }
        /*
        public virtual Card GetCard(string cardId)
        {
            var url = string.Format("{0}/{1}", Urls.Cards, cardId);

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Card>.MapFromJson(response);
        }
        */
        public virtual Card DeleteCard(string cardId)
        {
            var url = string.Format("{0}/{1}", Urls.Cards, cardId);

            var response = Requestor.Delete(url, AuthUser, AuthPassword);

            return Mapper<Card>.MapFromJson(response);
        }


        public virtual SearchResults<Card> ListCards(int page = 1, int page_size = 20, string reference = null)
        {
            var url = Urls.Cards;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", page_size.ToString());
            if (reference != null)
            {
                url = ParameterBuilder.ApplyParameterToUrl(url, "reference", reference);
            }

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Card>.MapCollectionFromJson(response);
        }

        public virtual Bank CreateBank(BankOptions options)
        {
            var response = Requestor.PostJson(
                Urls.Banks,
                ParameterBuilder.BuildJsonPostParameters(options),
                AuthUser,
                AuthPassword);

            return Mapper<Bank>.MapFromJson(response);
        }
        
        /*
        public virtual Bank GetBank(string bankId)
        {
            var url = string.Format("{0}/{1}", Urls.Banks, bankId);

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Bank>.MapFromJson(response);
        }
        */

        public virtual Bank DeleteBank(string bankId)
        {
            var url = string.Format("{0}/{1}", Urls.Banks, bankId);

            var response = Requestor.Delete(url, AuthUser, AuthPassword);

            return Mapper<Bank>.MapFromJson(response);
        }

        public virtual SearchResults<Bank> ListBanks(int page = 1, int page_size = 20, string reference = null)
        {
            var url = Urls.Banks;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", page_size.ToString());
            if (reference != null)
            {
                url = ParameterBuilder.ApplyParameterToUrl(url, "reference", reference);
            }

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Bank>.MapCollectionFromJson(response);
        }

        public virtual Token GetToken(string tokenId)
        {
            var url = string.Format("{0}/{1}", Urls.Tokens, tokenId);

            var response = Requestor.GetString(url, AuthUser, AuthPassword);

            return Mapper<Token>.MapFromJson(response);
        }
     
        public virtual Token CreateToken(TokenOptions t)
        {
            var response = Requestor.PostString(Urls.Tokens, ParameterBuilder.BuildPostParameters(t), AuthUser, AuthPassword);

            return Mapper<Token>.MapFromJson(response);
        }

	}
}