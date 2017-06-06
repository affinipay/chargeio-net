using System;
using ChargeIO.Infrastructure;
using ChargeIO.Models;
using ChargeIO.Services.PaymentMethods;
using ChargeIO.Services.Transactions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChargeIO.Tests
{
    [TestFixture]
    public class TransactionServiceTest
    {
        private TransactionService _transactionService;
        private PaymentMethodService _paymentMethodService;
        private ChargeOptions _defaultChargeOptions;
        private string _rand;

        [SetUp]
        public void TestInitialize()
        {
            _defaultChargeOptions = new ChargeOptions()
            {
                Method = new CardOptions()
                {
                    Name = "FirstName LastName",
                    Address1 = "123 Main St.",
                    Address2 = "Apt #3",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "78759",
                    Number = "4111111111111111",
                    Cvv = "123",
                    ExpMonth = 3,
                    ExpYear = 2018
                }
            };

            _rand = new Random().Next().ToString();
            _transactionService = new TransactionService();
            _paymentMethodService = new PaymentMethodService();
        }

        [Test]
        public void TestCharge()
        {
            var c = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 345,
                Method = new CardOptions()
                {
                    Name = "FirstName LastName",
                    Address1 = "123 Main St.",
                    Address2 = "Apt #3",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "78759",
                    Number = "4111111111111111",
                    Cvv = "123",
                    ExpMonth = 3,
                    ExpYear = 2018
                }
            });

            Assert.NotNull(c.Id);
            Assert.AreEqual("AUTHORIZED", c.Status);
            Assert.NotNull(c.AccountId);
            Assert.AreEqual("CHARGE", c.Type);
            Assert.IsNull(c.FailureCode);
            Assert.IsTrue(c.AutoCapture);
            Assert.IsTrue(c.AmountInCents == 345);
            Assert.IsNull(c.GratuityInCents);
            Assert.AreEqual("USD", c.Currency);
            Assert.IsNull(c.Reference);
            Assert.AreEqual("MATCHED", c.CvvResult);
            Assert.AreEqual("ADDRESS_AND_POSTAL_CODE", c.AvsResult);
            Assert.NotNull(c.AuthorizationCode);
        }

        [Test]
        public void TestRefund()
        {
            var charge = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "Refund reference",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                }
            });

            var r = _transactionService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 55,
                Reference = "refund1"
            });
            Assert.IsTrue(r.AmountInCents == 55);
            Assert.IsTrue(r.AutoCapture);
            var r1 = _transactionService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 45,
                Reference = "refund2"
            });
            Assert.IsTrue(r1.AmountInCents == 45);

            var c = (Charge)_transactionService.GetTransaction(charge.Id);
            Assert.IsTrue(c.AmountInCentsRefunded == 100);
        }

        [Test]
        public void TestCustomData()
        {
            var charge = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                },
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });

            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == true);

            var r = _transactionService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 50,
                Reference = "refund for invoice",
                Data = new RefundData()
                {
                    Date = DateTime.Now,
                    Reason = "didn't like the merchandise"
                }
            });
            Assert.IsTrue(r.AmountInCents == 50);

            var c = (Charge)_transactionService.GetTransaction(charge.Id);
            var invoiceData = c.Data.ToObject<InvoiceData>();

            Assert.IsTrue(invoiceData.Number == "123-ABC");
            Assert.IsTrue(invoiceData.AmountInCents == 200);

        }

        [Test]
        public void TestAuthorizeAndVoid()
        {
            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                },
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == false);

            var voided = (Charge)_transactionService.Void(charge.Id, "VOIDREF");

            Assert.IsTrue(voided.Status == "VOIDED");
            Assert.IsTrue(voided.VoidReference == "VOIDREF");
        }

        [Test]
        public void TestAuthorizeAndCapture()
        {
            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                },
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == false);

            var captured = _transactionService.Capture(charge.Id, 70, "CAPTUREREF");

            Assert.IsTrue(captured.AmountInCents == 70);
            Assert.IsTrue(captured.Status == "COMPLETED");
            Assert.IsTrue(captured.CaptureReference == "CAPTUREREF");
        }

        [Test]
        public void TestVoidRefund()
        {
            var charge = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");

            var r = _transactionService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 50,
                Reference = "Refund Ref"
            });
            Assert.IsTrue(r.Status == "AUTHORIZED");
            Assert.AreEqual("Refund Ref", r.Reference);

            r = (Refund)_transactionService.Void(r.Id, "Void Refund Ref");
            Assert.IsTrue(r.Status == "VOIDED");
            Assert.AreEqual("Refund Ref", r.Reference);
            Assert.AreEqual("Void Refund Ref", r.VoidReference);

            charge = (Charge)_transactionService.GetTransaction(charge.Id);
            Assert.AreEqual(0, charge.AmountInCentsRefunded);
        }

        [Test]
        public void TestChargeBank()
        {
            var c = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 426,
                Method = new BankOptions()
                {
                    Name = "FirstName LastName",
                    Address1 = "123 Main St.",
                    Address2 = "Apt #3",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "78759",
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    AccountType = "CHECKING"
                }
            });
            Assert.AreEqual("AUTHORIZED", c.Status);
            Assert.NotNull(c.Id);
            Assert.AreEqual(426, c.AmountInCents);
            Assert.AreEqual("USD", c.Currency);

            c = (Charge)_transactionService.Void(c.Id, "Mistake");
            Assert.AreEqual("VOIDED", c.Status);
        }

        [Test]
        public void TestRefundBank()
        {
            var c = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 426,
                Method = new BankOptions()
                {
                    Name = "FirstName LastName",
                    Address1 = "123 Main St.",
                    Address2 = "Apt #3",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "78759",
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    AccountType = "CHECKING"
                }
            });
            Assert.AreEqual("AUTHORIZED", c.Status);
            Assert.NotNull(c.Id);

            var r = _transactionService.Refund(c.Id, new RefundOptions()
            {
                AmountInCents = 25
            });
            Assert.AreEqual("PENDING", r.Status);
            Assert.AreEqual(25, r.AmountInCents);

            c = (Charge)_transactionService.GetTransaction(c.Id);
            Assert.AreEqual(25, c.AmountInCentsRefunded);
        }

        [Test]
        public void TestChargeUsingCardToken()
        {
            var card = _paymentMethodService.CreateCard(new CardOptions()
            {
                Name = "John Doe",
                Number = "378282246310005",
                ExpMonth = 12,
                ExpYear = 2020,
                Address1 = "123 Main Dr",
                Address2 = "Suite 300",
                Cvv = "123",
                PostalCode = "78759"
            });
            Assert.NotNull(card.Id);

            var c = _transactionService.Charge(new ChargeOptions()
            {
                AmountInCents = 426,
                Method = new TokenReferenceOptions() {
                    TokenId = card.Id
                }
            });
            Assert.AreEqual("AUTHORIZED", c.Status);
            Assert.NotNull(c.Id);
            Assert.IsTrue(c.PaymentMethod is Card);

            card = (Card)c.PaymentMethod;
            Assert.AreEqual("John Doe", card.Name);
            Assert.AreEqual("***********0005", card.Number);
            Assert.AreEqual(12, card.ExpMonth);
            Assert.AreEqual(2020, card.ExpYear);
            Assert.AreEqual("123 Main Dr", card.Address1);
            Assert.AreEqual("Suite 300", card.Address2);
            Assert.AreEqual("78759", card.PostalCode);
        }

        [Test]
        public void TestListCharges()
        {
            var transactions = _transactionService.Transactions();
            Assert.IsTrue(transactions.PageSize == 20);
        }

        [Test]
        public void TestSearchCharges()
        {
            //TODO: perform some charges and then search for specific field values
            var rand = new Random().Next().ToString();

            var transactions = _transactionService.Transactions(
                1, //page
                20, //page_size
                "Ref", //search string
                null, // field search string
                DateTime.Now.AddYears(-1), //from
                DateTime.Now, //to
                null, // accountId
                null //order_by
            );

            Assert.IsTrue(transactions.PageSize == 20);
        }

        [Test]
        public void TestChargeInvalidCard()
        {
            try
            {
                _transactionService.Authorize(new ChargeOptions()
                {
                    AmountInCents = 100,
                    Currency = "USD",
                    Method = new CardOptions()
                    {
                        Name = "John Doe",
                        Number = "4242424242424241",
                        ExpMonth = 12,
                        ExpYear = 2020,
                        Address1 = "123 Main Dr",
                        Cvv = "123",
                        PostalCode = "78759"
                    }
                });
            }
            catch (ChargeIoException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.AreEqual("Card number is invalid", ex.Message);
                Assert.AreEqual("error", ex.Errors[0].Level);
                Assert.AreEqual("card_number_invalid", ex.Errors[0].Code);
            }
        }

        [Test]
        public void TestCaptureGratuity()
        {
            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 2000,
                Currency = "USD",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");

            const string signatureData = "[{\"x\":[100],\"y\":[100]}]";
            charge = _transactionService.Capture(charge.Id, new CaptureOptions()
            {
                AmountInCents = 2000,
                GratuityInCents = 400,
                Signature = new TransactionSignature()
                {
                    MimeType = "chargeio/jsignature",
                    Data = signatureData
                }
            });

            Assert.AreEqual(2000, charge.AmountInCents);
            Assert.AreEqual(400, charge.GratuityInCents);
            Assert.NotNull(charge.SignatureId);

            var signature = _transactionService.GetSignature(charge.SignatureId);
            Assert.NotNull(signature);
            Assert.AreEqual("chargeio/jsignature", signature.MimeType);
            Assert.AreEqual(signatureData, signature.Data);
        }

        [Test]
        public void TestAuthorizeGratuity()
        {
            const string signatureData = "[{\"x\":[100],\"y\":[100]}]";
            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 2000,
                Currency = "USD",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                },
                GratuityInCents = 400,
                Signature = new TransactionSignature()
                {
                    MimeType = "chargeio/jsignature",
                    Data = signatureData
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.AreEqual(2000, charge.AmountInCents);
            Assert.AreEqual(400, charge.GratuityInCents);
            Assert.NotNull(charge.SignatureId);

            var signature = _transactionService.GetSignature(charge.SignatureId);
            Assert.NotNull(signature);
            Assert.AreEqual("chargeio/jsignature", signature.MimeType);
            Assert.AreEqual(signatureData, signature.Data);
        }

        [Test]
        public void TestAuthorizeAndSign()
        {
            const string signatureData = "[{\"x\":[100],\"y\":[100]}]";
            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 2000,
                Currency = "USD",
                Method = new CardOptions()
                {
                    Name = "John Doe",
                    Number = "378282246310005",
                    ExpMonth = 12,
                    ExpYear = 2020,
                    Address1 = "123 Main Dr",
                    Address2 = "Suite 300",
                    Cvv = "123",
                    PostalCode = "78759"
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");

            charge = (Charge)_transactionService.Sign(charge.Id, new SignatureOptions()
            {
                MimeType = "chargeio/jsignature",
                Data = signatureData,
                GratuityInCents = 400
            });

            Assert.AreEqual(2000, charge.AmountInCents);
            Assert.AreEqual(400, charge.GratuityInCents);
            Assert.NotNull(charge.SignatureId);

            var signature = _transactionService.GetSignature(charge.SignatureId);
            Assert.NotNull(signature);
            Assert.AreEqual("chargeio/jsignature", signature.MimeType);
            Assert.AreEqual(signatureData, signature.Data);
        }

        [Test]
        public void TestHolds()
        {
            var token = _paymentMethodService.CreateToken(new TokenOptions()
            {
                Type = "card",
                CardNumber = "378282246310005",
                CardCvv = "123",
                CardExpirationMonth = 12,
                CardExpirationYear = 2020,
                Name = "A Customer",
                Address1 = "123 Main Dr",
                City = "Austin",
                State = "TX",
                PostalCode = "78759",
                Country = "US"
            });

            var charge = _transactionService.Authorize(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                Method = new TokenReferenceOptions()
                {
                    TokenId = token.Id
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == false);


            var holds = _transactionService.Holds();
            Assert.IsTrue(holds.TotalEntries >= 1);
            Assert.AreEqual(charge.Id, holds[0].Id);


            charge = _transactionService.Capture(charge.Id, new CaptureOptions()
            {
                AmountInCents = 100,
                CaptureTime = "NEXT_AUTO_CAPTURE"
            });
            Assert.AreEqual("AUTHORIZED", charge.Status);
            Assert.IsTrue(charge.AutoCapture);


            holds = _transactionService.Holds();
            Assert.IsTrue(holds.TotalEntries >= 0);
            Assert.AreNotEqual(charge.Id, holds[0].Id);
        }
    }

    public class InvoiceData
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }
    }

    public class RefundData
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
