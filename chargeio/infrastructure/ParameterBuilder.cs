using System.Net;
using System.Reflection;
using Newtonsoft.Json;

namespace chargeio
{
	internal static class ParameterBuilder
	{
		public static string ApplyAllParameters(object obj, string url)
		{
			if (obj == null) return url;

			var newUrl = url;

			foreach (var property in obj.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
			{
				foreach (var attribute in property.GetCustomAttributes(false))
				{
					if (attribute.GetType() != typeof(JsonPropertyAttribute)) continue;

					var JsonPropertyAttribute = (JsonPropertyAttribute)attribute;

					var value = property.GetValue(obj, null);

					if (value != null)
						newUrl = ApplyParameterToUrl(newUrl, JsonPropertyAttribute.PropertyName, value.ToString());
				}
			}

			return newUrl;
		}
        
        public static string BuildPostParameters(object obj)
        {
            if (obj == null) return null;

            var newUrl = "";

            foreach (var property in obj.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute.GetType() != typeof(JsonPropertyAttribute)) continue;

                    var JsonPropertyAttribute = (JsonPropertyAttribute)attribute;

                    var value = property.GetValue(obj, null);

                    if (value != null)
                        newUrl = ApplyParameterToUrl(newUrl, JsonPropertyAttribute.PropertyName, value.ToString());
                }
            }

            return newUrl.Substring(1); //gets rid of the '?'
        }

		public static string ApplyParameterToUrl(string url, string argument, string value)
		{
			var token = "&";

			if (!url.Contains("?"))
				token = "?";

			return string.Format("{0}{1}{2}={3}", url, token, argument, WebUtility.UrlEncode(value));
		}


        public static string BuildJsonPostParameters(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
