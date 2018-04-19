using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ChargeIO.Infrastructure
{
	public static class Configuration
	{
        public static IConfigurationRoot AppCfg { get; }
		public static string SecretKey { get; }
		public static string ApiUrl { get; }
		public static long HttpTimeout { get; }
		public static Version AssemblyVersion { get; }

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            AppCfg = builder.Build();
            SecretKey = AppCfg["ChargeIOSecretKey"];

            string apiUrl = AppCfg["ChargeIOApiUrl"];
            ApiUrl = apiUrl != null ? apiUrl : "https://api.chargeio.com/v1";

            long timeout = long.TryParse(AppCfg["ChargeIOHTTPTimeout"], out timeout) ? timeout : 30000;
            HttpTimeout = timeout;

	        AssemblyVersion = typeof(Configuration).GetTypeInfo().Assembly.GetName().Version;
        }
	}
}
