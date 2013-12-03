using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace ChargeIO
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
            
            foreach (var tkn in allTokens)
                results.Add(Mapper<T>.MapFromJson(tkn.ToString()));

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
            foreach (var tkn in allTokens)
                results.Add(MapFromJson(tkn.ToString()));
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
            IPaymentMethod method = (IPaymentMethod)value;
            serializer.Serialize(writer, method);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
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
            TokenReferenceOptions options = (TokenReferenceOptions)value;
            writer.WriteValue(options.TokenId);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            TokenReferenceOptions options = new TokenReferenceOptions();
            options.TokenId = (string)reader.Value;
            return options;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TokenReferenceOptions);
        }
    }
}