using System;
using System.Collections.Generic;
using ChargeIO.Infrastructure;
using ChargeIO.Models;

namespace ChargeIO.Services.Transactions
{
	public class TransactionService
	{
        private string SecretKey { get; }

        public TransactionService(string secretKey = "")
		{
		    if (secretKey == null)
		    {
		        SecretKey = Configuration.SecretKey;
		    }
		    else
		    {
		        SecretKey = secretKey.Length > 0 ? secretKey : Configuration.SecretKey;
		    }
		}

        public virtual Charge Charge(ChargeOptions options)
        {
            options.AutoCapture = true;

            var response = Requestor.PostJson(
                Urls.Charges,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Charge>.MapFromJson(response);
        }

        public virtual Charge Authorize(ChargeOptions options)
        {
            options.AutoCapture = false;

            var response = Requestor.PostJson(
                Urls.Charges,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Charge>.MapFromJson(response);
        }

        public virtual Transaction Void(string transactionId, string reference = null)
        {
            var url = string.Format("{0}/{1}/void", Urls.Transactions, transactionId);
            var reqparams = new Dictionary<string, string> {{"reference", reference}};
            var response = Requestor.PostJson(url, 
                ParameterBuilder.BuildJsonPostParameters(reqparams), SecretKey);

            return TransactionMapper.MapFromJson(response);
        }

        public virtual Charge Capture(string chargeId, int amountInCents, string reference = null)
        {
            var url = string.Format("{0}/{1}/capture", Urls.Charges, chargeId);

            var response = Requestor.PostJson(url, 
                ParameterBuilder.BuildJsonPostParameters(new CaptureOptions() 
                {
                    AmountInCents = amountInCents,
                    Reference = reference
                }), SecretKey);

            return Mapper<Charge>.MapFromJson(response);
        }

        public virtual Charge Capture(string chargeId, CaptureOptions options)
        {
            var url = string.Format("{0}/{1}/capture", Urls.Charges, chargeId);

            var response = Requestor.PostJson(url, ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Charge>.MapFromJson(response);
        }

        public virtual Refund Refund(string chargeId, RefundOptions options)
        {
            var url = string.Format("{0}/{1}/refund", Urls.Charges, chargeId);

            var response = Requestor.PostJson(url, ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Refund>.MapFromJson(response);
        }

        public virtual Refund Refund(string chargeId, int? refundAmountInCents, string reference = null, object data = null)
        {
            var url = string.Format("{0}/{1}/refund", Urls.Charges, chargeId);

            var options = new RefundOptions
            {
                AmountInCents = refundAmountInCents,
                Reference = reference,
                Data = data
            };
            var response = Requestor.PostJson(url, ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Refund>.MapFromJson(response);
        }

        public virtual Credit Credit(CreditOptions options)
        {
            var response = Requestor.PostJson(
                Urls.Credits,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);

            return Mapper<Credit>.MapFromJson(response);
        }

        public virtual Transaction GetTransaction(string transactionId)
        {
            var url = string.Format("{0}/{1}", Urls.Transactions, transactionId);
            var response = Requestor.GetString(url, SecretKey);
            return TransactionMapper.MapFromJson(response);
        }

        public virtual SearchResults<Transaction> Transactions(
            int page = 1, 
            int pageSize = 20, 
            string q = null,
            string qf = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string accountId = null,
            string orderBy = null)
        {
            var url = Urls.Transactions;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", pageSize.ToString());

            if (!string.IsNullOrEmpty(q))
                url = ParameterBuilder.ApplyParameterToUrl(url, "q", q);

            if (!string.IsNullOrEmpty(qf))
                url = ParameterBuilder.ApplyParameterToUrl(url, "qf", qf);

            if (startDate != null)
                url = ParameterBuilder.ApplyParameterToUrl(url, "start_date", ((DateTime) startDate).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            if (endDate != null)
                url = ParameterBuilder.ApplyParameterToUrl(url, "end_date", ((DateTime)endDate).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            
            if (!string.IsNullOrEmpty(accountId))
                url = ParameterBuilder.ApplyParameterToUrl(url, "account_id", accountId);

            if (!string.IsNullOrEmpty(orderBy))
                url = ParameterBuilder.ApplyParameterToUrl(url, "order_by", orderBy);

            var response = Requestor.GetString(url, SecretKey);

            return TransactionMapper.MapCollectionFromJson(response);
        }

        public virtual SearchResults<Transaction> Holds(
            int page = 1,
            int pageSize = 20,
            string accountId = null)
        {
            var url = Urls.Holds;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", pageSize.ToString());

            if (!string.IsNullOrEmpty(accountId))
                url = ParameterBuilder.ApplyParameterToUrl(url, "account_id", accountId);

            var response = Requestor.GetString(url, SecretKey);

            return TransactionMapper.MapCollectionFromJson(response);
        }

        public virtual Transaction Sign(string transactionId, SignatureOptions options)
        {
            var url = string.Format("{0}/{1}/sign", Urls.Transactions, transactionId);
            var response = Requestor.PostJson(url, ParameterBuilder.BuildJsonPostParameters(options), SecretKey);
            return TransactionMapper.MapFromJson(response);
        }

        public virtual TransactionSignature GetSignature(string signatureId)
        {
            var url = string.Format("{0}/{1}", Urls.Signatures, signatureId);
            var response = Requestor.GetString(url, SecretKey);
            return Mapper<TransactionSignature>.MapFromJson(response);
        }
    }
}