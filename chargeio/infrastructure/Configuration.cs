using System;
using Microsoft.Extensions.Configuration;

namespace chargeio
{
	public static class Configuration
	{
        public static IConfigurationRoot Get { get; }
		public static string SecretKey { get; set; }
		public static string ApiUrl { get; set; }
		public static string ApiVersion { get; set; }
		public static Int64 HttpTimeout { get; set; }

        static Configuration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Get = configuration.Build();
            SecretKey = Get["ChargeIOSecretKey"];
            ApiUrl = Get["ChargeIOApiUrl"];
            ApiVersion = Get["ApiVersion"];
            HttpTimeout = Int64.Parse(Get["ChargeIOHTTPTimeout"]);
        }
	}
}
