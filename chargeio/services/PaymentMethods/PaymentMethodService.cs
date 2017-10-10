using ChargeIO.Infrastructure;
using ChargeIO.Models;

namespace ChargeIO.Services.PaymentMethods
{
	public class PaymentMethodService : ServiceBase
	{
        public PaymentMethodService(string secretKey = "") : base(secretKey)
        {
            
        }

        public virtual Card CreateCard(object options)
        {
            var response = Requestor.PostJson(
                Urls.Cards,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Card>.MapFromJson(response);
        }

        public virtual Card DeleteCard(string cardId)
        {
            var url = string.Format("{0}/{1}", Urls.Cards, cardId);

            var response = Requestor.Delete(url, SecretKey);

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

            var response = Requestor.GetString(url, SecretKey);

            return Mapper<Card>.MapCollectionFromJson(response);
        }

        public virtual Bank CreateBank(object options)
        {
            var response = Requestor.PostJson(
                Urls.Banks,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Bank>.MapFromJson(response);
        }

        public virtual Bank DeleteBank(string bankId)
        {
            var url = string.Format("{0}/{1}", Urls.Banks, bankId);

            var response = Requestor.Delete(url, SecretKey);

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

            var response = Requestor.GetString(url, SecretKey);

            return Mapper<Bank>.MapCollectionFromJson(response);
        }

        public virtual Token GetToken(string tokenId)
        {
            var url = string.Format("{0}/{1}", Urls.Tokens, tokenId);

            var response = Requestor.GetString(url, SecretKey);

            return Mapper<Token>.MapFromJson(response);
        }
     
        public virtual Token CreateToken(TokenOptions t)
        {
            var response = Requestor.PostString(Urls.Tokens, ParameterBuilder.BuildPostParameters(t), SecretKey);

            return Mapper<Token>.MapFromJson(response);
        }
	}
}