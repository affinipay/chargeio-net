using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChargeIO;

namespace ChargeIO.Test
{
    [TestFixture]
    public class PaymentMethodServiceTest
    {
        PaymentMethodService defaultService;

        [SetUp]
        public void TestInitialize()
        {
            defaultService = new PaymentMethodService();
        }

        [Test]
        public void TestCreateCard()
        {
            Card c = defaultService.CreateCard(new CardOptions()
            {
                Number = "4242424242424242",
                ExpMonth = 12,
                ExpYear = 2016,
                Name = "A Customer",
                Email = "customer@example.com",
                Phone = "111-222-3344",
                Description = "Airline Card",
                Reference = "Customer123"
            });
            Assert.IsTrue(c.Number == "************4242");
            Assert.IsTrue(c.CardType == "VISA");
            Assert.IsTrue(c.ExpMonth == 12);
            Assert.IsTrue(c.ExpYear == 2016);
            Assert.AreEqual("customer@example.com", c.Email);
            Assert.AreEqual("111-222-3344", c.Phone);
            Assert.AreEqual("Airline Card", c.Description);
            Assert.AreEqual("Customer123", c.Reference);
        }

        [Test]
        public void TestCreateCardFromToken()
        {
            Token t = defaultService.CreateToken(new TokenOptions()
            {
                Type = "card",
                CardNumber = "4242424242424242",
                CardExpirationMonth = 12,
                CardExpirationYear = 2016,
                Name = "A Customer",
                Address1 = "123 Payments Way",
                PostalCode = "78730",
                Reference = "Customer456"
            });
            Assert.NotNull(t.Id);

            Card c = defaultService.CreateCard(new SavedPaymentFromTokenOptions()
            {
                TokenId = t.Id
            });
            Assert.IsTrue(c.Number == "************4242");
            Assert.IsTrue(c.CardType == "VISA");
            Assert.IsTrue(c.ExpMonth == 12);
            Assert.IsTrue(c.ExpYear == 2016);
            Assert.AreEqual("A Customer", c.Name);
            Assert.AreEqual("123 Payments Way", c.Address1);
            Assert.AreEqual("78730", c.PostalCode);
            Assert.AreEqual("Customer456", c.Reference);
        }

        [Test]
        public void TestDeleteCard()
        {
            Card c = defaultService.CreateCard(new CardOptions()
            {
                Number = "4242424242424242",
                ExpMonth = 12,
                ExpYear = 2016,
                Name = "A Customer"
            });
            Card deleted = defaultService.DeleteCard(c.Id);

            Assert.IsTrue(deleted.Number == "************4242");
        }

        [Test]
        public void TestListCards()
        {
            SearchResults<Card> cards = defaultService.ListCards();
            Assert.IsTrue(cards.Page == 1);
        }


        [Test]
        public void TestCreateBank()
        {
            Bank b = defaultService.CreateBank(new BankOptions()
            {
                AccountNumber = "10333257392394",
                AccountType = "CHECKING",
                RoutingNumber = "111000025",
                Name = "A Customer",
                Description = "Primary Checking",
                Reference = "Customer123"
            });
            Assert.IsTrue(b.AccountNumber == "**********2394");
            Assert.IsTrue(b.RoutingNumber == "******025");
            Assert.IsTrue(b.AccountType == "CHECKING");
            Assert.AreEqual("Primary Checking", b.Description);
            Assert.AreEqual("Customer123", b.Reference);
        }

        [Test]
        public void TestCreateBankFromToken()
        {
            Token t = defaultService.CreateToken(new TokenOptions()
            {
                Type = "bank",
                BankAccountNumber = "10333257392394",
                BankAccountType = "CHECKING",
                BankRoutingNumber = "111000025",
                Name = "A Customer",
                Description = "Primary Checking",
                Reference = "Customer123"
            });
            Assert.NotNull(t.Id);

            Bank b = defaultService.CreateBank(new SavedPaymentFromTokenOptions()
            {
                TokenId = t.Id
            });
            Assert.IsTrue(b.AccountNumber == "**********2394");
            Assert.IsTrue(b.RoutingNumber == "******025");
            Assert.IsTrue(b.AccountType == "CHECKING");
            Assert.AreEqual("Primary Checking", b.Description);
            Assert.AreEqual("Customer123", b.Reference);
        }

        [Test]
        public void TestDeleteBank()
        {
            Bank b = defaultService.CreateBank(new BankOptions()
            {
                AccountNumber = "10333257392394",
                AccountType = "CHECKING",
                RoutingNumber = "111000025",
                Name = "A Customer"
            });
            Bank deleted = defaultService.DeleteBank(b.Id);
            Assert.IsTrue(deleted.AccountNumber == "**********2394");
        }

        [Test]
        public void TestListBanks()
        {
            SearchResults<Bank> banks = defaultService.ListBanks();
            Assert.IsTrue(banks.Page == 1);
        }

        [Test]
        public void TestCreateAndRetrieveToken()
        {
            Token t = defaultService.CreateToken(new TokenOptions()
            {
                Type = "bank",
                BankAccountNumber = "10333257392394",
                BankAccountType = "CHECKING",
                BankRoutingNumber = "111000025",
                Name = "A Customer"
            });
            Assert.IsTrue(t.BankAccountNumber == "**********2394");
            Assert.IsTrue(t.BankRoutingNumber == "******025");
            Assert.IsTrue(t.BankAccountType == "CHECKING");
            Token retrieved = defaultService.GetToken(t.Id);
            Assert.IsTrue(t.BankAccountNumber == retrieved.BankAccountNumber);
        }
    }
}
