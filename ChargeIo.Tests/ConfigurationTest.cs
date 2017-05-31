using ChargeIo.Infrastructure;
using NUnit.Framework;

namespace ChargeIo.Tests
{
    public class ConfigurationTest
    {
        [Test]
        public void TestApiUrl()
        {
            Assert.AreNotEqual("<Your OAuth Secret>", $"{Configuration.SecretKey}", "You must set 'ChargeIOSecretKey' in appsettings.json");
            Assert.AreEqual("https://api.chargeio.com", $"{Configuration.ApiUrl}");
        }
    }
}
