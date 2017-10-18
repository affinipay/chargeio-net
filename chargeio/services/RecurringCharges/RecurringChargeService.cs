using ChargeIO.Infrastructure;
using ChargeIO.Models;

namespace ChargeIO.Services.RecurringCharges
{
	public class RecurringChargeService : ServiceBase
	{
        public RecurringChargeService(string secretKey = null) : base(secretKey)
        {
            
        }

        public virtual RecurringCharge RecurringCharge(RecurringChargeOptions options)
        {
            var response = Requestor.PostJson(
                Urls.RecurringCharges,
                ParameterBuilder.BuildJsonPostParameters(options), SecretKey);
            return Mapper<RecurringCharge>.MapFromJson(response);
        }

        public virtual RecurringCharge GetRecurringCharge(string id)
        {
            var url = string.Format("{0}/{1}", Urls.RecurringCharges, id);
            var response = Requestor.GetString(url, SecretKey);
            return Mapper<RecurringCharge>.MapFromJson(response);
        }

        public virtual RecurringCharge UpdateRecurringCharge(string id, RecurringChargeOptions options)
        {
            var url = string.Format("{0}/{1}", Urls.RecurringCharges, id);
            var response = Requestor.PutJson(url, ParameterBuilder.BuildJsonPostParameters(options), SecretKey);
            return Mapper<RecurringCharge>.MapFromJson(response);
        }

        public virtual RecurringCharge CancelRecurringCharge(string id)
        {
            var url = string.Format("{0}/{1}/cancel", Urls.RecurringCharges, id);
            var response = Requestor.PostJson(url, "", SecretKey);
            return Mapper<RecurringCharge>.MapFromJson(response);
        }

        public virtual RecurringCharge DeleteRecurringCharge(string id)
        {
            var url = string.Format("{0}/{1}", Urls.RecurringCharges, id);
            var response = Requestor.Delete(url, SecretKey);
            return Mapper<RecurringCharge>.MapFromJson(response);
        }

        public virtual SearchResults<RecurringCharge> RecurringCharges(
            int page = 1,
            int pageSize = 20,
            string accountId = null,
            string status = null,
            string orderBy = null)
        {
            var url = Urls.RecurringCharges;
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", pageSize.ToString());

            if (!string.IsNullOrEmpty(accountId))
                url = ParameterBuilder.ApplyParameterToUrl(url, "account_id", accountId);

            if (!string.IsNullOrEmpty(status))
                url = ParameterBuilder.ApplyParameterToUrl(url, "status", status);

            if (!string.IsNullOrEmpty(orderBy))
                url = ParameterBuilder.ApplyParameterToUrl(url, "order_by", orderBy);

            var response = Requestor.GetString(url, SecretKey);
            return Mapper<RecurringCharge>.MapCollectionFromJson(response);
        }

        public virtual RecurringChargeOccurrence GetOccurrence(string recurringChargeId, string occurrenceId)
        {
            var url = string.Format("{0}/{1}/occurrences/{2}", Urls.RecurringCharges, recurringChargeId, occurrenceId);
            var response = Requestor.GetString(url, SecretKey);
            return Mapper<RecurringChargeOccurrence>.MapFromJson(response);
        }

        public virtual RecurringChargeOccurrence PayOccurrence(string recurringChargeId, string occurrenceId)
        {
            var url = string.Format("{0}/{1}/occurrences/{2}/pay", Urls.RecurringCharges, recurringChargeId, occurrenceId);
            var response = Requestor.PostJson(url, "", SecretKey);
            return Mapper<RecurringChargeOccurrence>.MapFromJson(response);
        }

        public virtual RecurringChargeOccurrence IgnoreOccurrence(string recurringChargeId, string occurrenceId)
        {
            var url = string.Format("{0}/{1}/occurrences/{2}/ignore", Urls.RecurringCharges, recurringChargeId, occurrenceId);
            var response = Requestor.PostJson(url, "", SecretKey);
            return Mapper<RecurringChargeOccurrence>.MapFromJson(response);
        }

        public virtual SearchResults<RecurringChargeOccurrence> Occurrences(
            string recurringChargeId,
            int page = 1,
            int pageSize = 20)
        {
            var url = string.Format("{0}/{1}/occurrences", Urls.RecurringCharges, recurringChargeId);
            url = ParameterBuilder.ApplyParameterToUrl(url, "page", page.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "page_size", pageSize.ToString());

            var response = Requestor.GetString(url, SecretKey);
            return Mapper<RecurringChargeOccurrence>.MapCollectionFromJson(response);
        }
	}
}