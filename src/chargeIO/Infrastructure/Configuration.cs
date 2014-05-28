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
	}
}
