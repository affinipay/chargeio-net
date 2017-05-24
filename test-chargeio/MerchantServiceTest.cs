using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using chargeio;

namespace test_chargeio
{
    [TestFixture]
    public class MerchantServiceTest
    {
        MerchantService merchantService;

        [SetUp]
        public void TestInitialize()
        {
            merchantService = new MerchantService();
        }

        [Test]
        public void TestMerchantProperties()
        {
            Merchant m = merchantService.GetMerchant();
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

            MerchantAccount a = m.MerchantAccounts[0];
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
            Merchant m = merchantService.GetMerchant();
            Assert.IsTrue(m != null);
            Merchant updated = merchantService.UpdateMerchant(new MerchantOptions()
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
            Merchant m = merchantService.GetMerchant();
            MerchantAccount a = m.MerchantAccounts[0];
            Assert.IsTrue(a != null);
            MerchantAccount updated = merchantService.UpdateMerchantAccount(a.Id, new MerchantAccountOptions()
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
            Merchant m = merchantService.GetMerchant();
            AchAccount a = m.ACHAccounts[0];
            Assert.IsTrue(a != null);
            AchAccount updated = merchantService.UpdateACHAccount(a.Id, new ACHAccountOptions()
            {
                Name = "the new bank account name",
                Primary = a.Primary,
                RequiredPaymentFields = a.RequiredPaymentFields
            });
            Assert.IsTrue(updated.Name == "the new bank account name");
        }
    }
}
