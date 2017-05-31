using System;
using Microsoft.Extensions.Configuration;

namespace ChargeIo.Infrastructure
{
	public static class Configuration
	{
        public static IConfigurationRoot Get { get; }
		public static string SecretKey { get; set; }
		public static string ApiUrl { get; set; }
		public static string ApiVersion { get; set; }
		public static long HttpTimeout { get; set; }

        static Configuration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Get = configuration.Build();
            SecretKey = Get["ChargeIOSecretKey"];
            ApiUrl = Get["ChargeIOApiUrl"];
            ApiVersion = Get["ApiVersion"];
            HttpTimeout = long.Parse(Get["ChargeIOHTTPTimeout"]);
        }
	}
}
