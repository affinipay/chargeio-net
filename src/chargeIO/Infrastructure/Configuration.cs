using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ChargeIO
{
	public static class Configuration
	{
		private static string _secretKey;
        private static string _apiUrl;
        private static int _http_timeout = 300 * 1000;

		internal static string GetSecretKey()
		{
			if (String.IsNullOrEmpty(_secretKey))
				_secretKey = ConfigurationManager.AppSettings["ChargeIOSecretKey"];

			return _secretKey;
		}

		public static void SetSecretKey(string newSecretKey)
		{
			_secretKey = newSecretKey;
		}

        internal static string GetApiUrl()
        {
            if (String.IsNullOrEmpty(_apiUrl))
                _apiUrl = ConfigurationManager.AppSettings["ChargeIOApiUrl"];

            if (String.IsNullOrEmpty(_apiUrl))
                _apiUrl = "https://api.chargeio.com/v1";

            return _apiUrl;
        }

        public static void SetApiUrl(string newApiUrl)
        {
            _apiUrl = newApiUrl;
        }

        public static void SetHTTPTimeout(int timeout)
        {
            _http_timeout = timeout;
        }

        internal static int GetHTTPTimeout()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ChargeIOHTTPTimeout"]))
                _http_timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ChargeIOHTTPTimeout"]);

            return _http_timeout;
        }

	}
}
