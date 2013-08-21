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
        public static SearchResults<object> MapCollectionFromJson(string json, string token = "results")
		{
            var results = new SearchResults<object>();

            var jObject = JObject.Parse(json);
            results.Page = jObject.Value<int>("page");
            results.PageSize = jObject.Value<int>("page_size");
            results.TotalEntries = jObject.Value<int>("total_entries");

			var allTokens = jObject.SelectToken(token);
            foreach (var tkn in allTokens)
                results.Add(MapFromJson(tkn.ToString()));
            return results;
		}
    
        public static object MapFromJson(string json, string parentToken = null)
        {
            var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();
            var jObject = JObject.Parse(jsonToParse);
            switch (jObject["type"].ToString())
            {
                case "CHARGE":
                    return JsonConvert.DeserializeObject<Charge>(jsonToParse);
                case "CREDIT":
                case "REFUND":
                    return JsonConvert.DeserializeObject<Refund>(jsonToParse);
            }

            return null;
        }
    }
}