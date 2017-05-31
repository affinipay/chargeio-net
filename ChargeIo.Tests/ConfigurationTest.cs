using System;
using ChargeIo.Infrastructure;
using NUnit.Framework;

namespace ChargeIo.Tests
{
    public class ConfigurationTest
    {
        [Test]
        public void TestApiUrl()
        {
            Assert.NotNull(Configuration.AssemblyVersion);
            Console.WriteLine("AssemblyVersion = [" + Configuration.AssemblyVersion + "]");
            Assert.AreNotEqual("<Your OAuth Secret>", $"{Configuration.SecretKey}", "You must set 'ChargeIOSecretKey' in appsettings.json");
            Assert.AreEqual("https://api.chargeio.com/v1", $"{Configuration.ApiUrl}");
        }
    }
}
