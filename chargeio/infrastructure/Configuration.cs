using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ChargeIo.Infrastructure
{
	public static class Configuration
	{
        public static IConfigurationRoot Get { get; }
		public static string SecretKey { get; set; }
		public static string ApiUrl { get; set; }
		public static long HttpTimeout { get; set; }
		public static Version AssemblyVersion { get; set; }

        static Configuration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Get = configuration.Build();
            SecretKey = Get["ChargeIOSecretKey"];
            ApiUrl = Get["ChargeIOApiUrl"];
            HttpTimeout = long.Parse(Get["ChargeIOHTTPTimeout"]);
	        AssemblyVersion = typeof(Configuration).GetTypeInfo().Assembly.GetName().Version;
        }
	}
}
