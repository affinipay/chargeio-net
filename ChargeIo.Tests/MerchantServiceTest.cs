using ChargeIo.Services.Merchant;
using NUnit.Framework;

namespace ChargeIo.Tests
{
    [TestFixture]
    public class MerchantServiceTest
    {
        private MerchantService _merchantService;

        [SetUp]
        public void TestInitialize()
        {
            _merchantService = new MerchantService();
        }

        [Test]
        public void TestMerchantProperties()
        {
            var m = _merchantService.GetMerchant();
            Assert.IsNotNull(m);
            Assert.IsNotNull(m.Id);
            Assert.IsNotNull(m.Created);
            Assert.IsNotNull(m.Modified);
            Assert.IsNotNull(m.Name);
            Assert.IsNotNull(m.ContactName);
            Assert.IsNotNull(m.ContactEmail);
            Assert.IsNotNull(m.ContactPhone);
            Assert.IsNotNull(m.Address1);
            Assert.IsNotNull(m.City);
            Assert.IsNotNull(m.State);
            Assert.IsNotNull(m.PostalCode);
            Assert.IsNotNull(m.Country);
            Assert.IsNotNull(m.Timezone);

            var a = m.MerchantAccounts[0];
            Assert.IsNotNull(a);
            Assert.IsNotNull(a.Id);
            Assert.IsNotNull(a.Created);
            Assert.IsNotNull(a.Modified);
            Assert.IsNotNull(a.Status);
            Assert.IsNotNull(a.Name);
            Assert.IsNotNull(a.Primary);
            Assert.IsNotNull(a.Currency);
            Assert.IsNotNull(a.CVVPolicy);
            Assert.IsNotNull(a.AVSPolicy);
            Assert.IsNotNull(a.IgnoreAVSFailureIfCVVMatch);
            Assert.IsNotNull(a.RequiredPaymentFields);
            Assert.IsNotNull(a.SwipeCVVPolicy);
            Assert.IsNotNull(a.SwipeAVSPolicy);
            Assert.IsNotNull(a.SwipeIgnoreAVSFailureIfCVVMatch);
            Assert.IsNotNull(a.SwipeRequiredPaymentFields);
            Assert.IsNotNull(a.TransactionAllowedCountries);
            Assert.IsNotNull(a.AcceptedCardTypes);
        }

        [Test]
        public void TestRenameMerchant()
        {
            var m = _merchantService.GetMerchant();
            Assert.IsTrue(m != null);
            var updated = _merchantService.UpdateMerchant(new MerchantOptions()
            {
                Name = "the new merchant name",
                ContactName = m.ContactName,
                ContactEmail = m.ContactEmail, 
                ContactPhone = m.ContactPhone,
                Address1 = m.Address1,
                Address2 = m.Address2,
                City = m.City,
                State = m.State,
                PostalCode = m.PostalCode,
                Country = m.Country,
                Timezone = m.Timezone,
                ApiAllowedIpAddressRanges = m.ApiAllowedIpAddressRanges

            });
            Assert.AreEqual("the new merchant name", updated.Name);
        }

        [Test]
        public void TestRenameAccount()
        {
            var m = _merchantService.GetMerchant();
            var a = m.MerchantAccounts[0];
            Assert.IsTrue(a != null);
            var updated = _merchantService.UpdateMerchantAccount(a.Id, new MerchantAccountOptions()
            {
                Name = "the new account name",
                Primary = a.Primary,
                CVVPolicy = a.CVVPolicy,
                AVSPolicy = a.AVSPolicy,
                IgnoreAVSFailureIfCVVMatch = a.IgnoreAVSFailureIfCVVMatch,
                RequiredPaymentFields = a.RequiredPaymentFields,
                SwipeCVVPolicy = a.SwipeCVVPolicy,
                SwipeAVSPolicy = a.SwipeAVSPolicy,
                SwipeIgnoreAVSFailureIfCVVMatch = a.SwipeIgnoreAVSFailureIfCVVMatch,
                SwipeRequiredPaymentFields = a.SwipeRequiredPaymentFields
            });
            Assert.IsTrue(updated.Name == "the new account name");
        }

        [Test]
        public void TestRenameBankAccount()
        {
            var m = _merchantService.GetMerchant();
            var a = m.AchAccounts[0];
            Assert.IsTrue(a != null);
            var updated = _merchantService.UpdateAchAccount(a.Id, new AchAccountOptions()
            {
                Name = "the new bank account name",
                Primary = a.Primary,
                RequiredPaymentFields = a.RequiredPaymentFields
            });
            Assert.IsTrue(updated.Name == "the new bank account name");
        }
    }
}
