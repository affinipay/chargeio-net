using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ChargeIO
{
	public static class Configuration
	{
		private static string _authUser;
        private static string _authPassword;
        private static string _apiUrl;

		internal static string GetAuthUser()
		{
			if (String.IsNullOrEmpty(_authUser))
				_authUser = ConfigurationManager.AppSettings["ChargeIOAuthUser"];

			return _authUser;
		}

		public static void SetAuthUser(string newAuthUser)
		{
			_authUser = newAuthUser;
		}

        internal static string GetAuthPassword()
        {
            if (String.IsNullOrEmpty(_authPassword))
                _authPassword = ConfigurationManager.AppSettings["ChargeIOAuthPassword"];

            return _authPassword;
        }

        public static void SetAuthPassword(string newAuthPassword)
        {
            _authPassword = newAuthPassword;
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
