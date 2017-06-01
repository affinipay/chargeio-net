using System;
using System.Linq;
using ChargeIo.Models;
using ChargeIo.Services.PaymentMethods;
using ChargeIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIo.Infrastructure
{
    public static class Mapper<T>
    {
        public static SearchResults<T> MapCollectionFromJson(string json, string token = "results")
        {
            var results = new SearchResults<T>();

            var jObject = JObject.Parse(json);
            results.Page = jObject.Value<int>("page");
            results.PageSize = jObject.Value<int>("page_size");
            results.TotalEntries = jObject.Value<int>("total_entries");

            var allTokens = jObject.SelectToken(token);

            results.AddRange(allTokens.Select(tkn => MapFromJson(tkn.ToString())));

            return results;
        }

        public static T MapFromJson(string json, string parentToken = null)
        {
            var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();
            return JsonConvert.DeserializeObject<T>(jsonToParse);
        }
    }
    
    public static class TransactionMapper
	{
        public static SearchResults<Transaction> MapCollectionFromJson(string json, string token = "results")
		{
            var results = new SearchResults<Transaction>();

            var jObject = JObject.Parse(json);
            results.Page = jObject.Value<int>("page");
            results.PageSize = jObject.Value<int>("page_size");
            results.TotalEntries = jObject.Value<int>("total_entries");

			var allTokens = jObject.SelectToken(token);
		    results.AddRange(allTokens.Select(tkn => MapFromJson(tkn.ToString())));
		    return results;
		}
    
        public static Transaction MapFromJson(string json, string parentToken = null)
        {
            var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();
            var jObject = JObject.Parse(jsonToParse);
            switch (jObject["type"].ToString())
            {
                case "CHARGE":
                    return JsonConvert.DeserializeObject<Charge>(jsonToParse);
                case "REFUND":
                    return JsonConvert.DeserializeObject<Refund>(jsonToParse);
                case "CREDIT":
                    return JsonConvert.DeserializeObject<Credit>(jsonToParse);
            }

            return null;
        }
    }

    public class PaymentMethodConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var method = (IPaymentMethod)value;
            serializer.Serialize(writer, method);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var json = jObject.ToString();
            switch (jObject["type"].ToString())
            {
                case "card":
                    return JsonConvert.DeserializeObject<Card>(json);
                case "bank":
                    return JsonConvert.DeserializeObject<Bank>(json);
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IPaymentMethod);
        }
    }

    public class TokenReferenceConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var options = (TokenReferenceOptions)value;
            writer.WriteValue(options.TokenId);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var options = new TokenReferenceOptions {TokenId = (string) reader.Value};
            return options;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TokenReferenceOptions);
        }
    }

    public class IsoDateConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
        public IsoDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}