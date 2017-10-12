using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ChargeIO.Infrastructure
{
	public static class Configuration
	{
        public static IConfigurationRoot Get { get; }
		public static string SecretKey { get; }
		public static string ApiUrl { get; }
		public static long HttpTimeout { get; }
		public static Version AssemblyVersion { get; }

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
