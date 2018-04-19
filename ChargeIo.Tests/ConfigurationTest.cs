using ChargeIO.Infrastructure;
using NUnit.Framework;

namespace ChargeIO.Tests
{
    public class ConfigurationTest
    {
        [Test]
        public void TestApiUrl()
        {
            Assert.NotNull(Configuration.AssemblyVersion);
            Assert.AreNotEqual("<Your OAuth Secret>", $"{Configuration.SecretKey}", "You must set 'ChargeIOSecretKey' in appsettings.json");
            Assert.AreEqual("https://api.chargeio.com/v1", $"{Configuration.ApiUrl}");
        }
    }
}
