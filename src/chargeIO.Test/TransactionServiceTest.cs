using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using ChargeIO;

namespace ChargeIO.Test
{
    [TestFixture]
    public class TransactionServiceTest
    {
        TransactionService defaultService;
        ChargeOptions defaultChargeOptions;
        string rand;

        [SetUp]
        public void TestInitialize()
        {
            defaultChargeOptions = new ChargeOptions()
            {
                CardName = "FirstName LastName",
                CardAddress1 = "123 Main St.",
                CardAddress2 = "Apt #3",
                CardCity = "Austin",
                CardState = "TX",
                CardPostalCode = "78759",
                CardNumber = "4111111111111111",
                CardCvv = "123",
                CardExpMonth = 3,
                CardExpYear = 2015
            };

            rand = new Random().Next().ToString();
            defaultService = new TransactionService();
        }

        [Test]
        public void TestCharge()
        {
            Charge c = defaultService.Charge(new ChargeOptions()
            {
                AmountInCents = 345,
                CardName = "FirstName LastName",
                CardAddress1 = "123 Main St.",
                CardAddress2 = "Apt #3",
                CardCity = "Austin",
                CardState = "TX",
                CardPostalCode = "78759",
                CardNumber = "4111111111111111",
                CardCvv = "123",
                CardExpMonth = 3,
                CardExpYear = 2015
            });
            Assert.IsTrue(c.AmountInCents == 345);
        }
        [Test]
        public void TestRefund()
        {
            Charge charge = defaultService.Charge(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "Sparrow rocks",
                CardName = "Enrico Brunetta",
                CardNumber = "378282246310005",
                CardExpMonth = 12,
                CardExpYear = 2016,
                CardAddress1 = "7101 Villa Maria Ln",
                CardCvv = "123",
                CardPostalCode = "78759"
            });

            Refund r = defaultService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 50,
                Reference = "enrico was here"
            });
            Assert.IsTrue(r.AmountInCents == 50);
            Refund r1 = defaultService.Refund(charge.Id, new RefundOptions()
            {
                AmountInCents = 50,
                Reference = "enrico was here"
            });
            Assert.IsTrue(r1.AmountInCents == 50);

            Charge c = defaultService.GetCharge(charge.Id);
            Assert.IsTrue(c.AmountInCentsRefunded == 100);
        }

        [Test]
        public void TestCustomData()
        {
            Charge charge = defaultService.Charge(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                CardName = "John Doe",
                CardNumber = "378282246310005",
                CardExpMonth = 12,
                CardExpYear = 2016,
                CardAddress1 = "123 Main Dr",
                CardAddress2 = "Suite 300",
                CardCvv = "123",
                CardPostalCode = "78759",
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });

            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == true);

            Refund r = defaultService.Refund(charge.Id, new RefundOptions()
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

            Charge c = defaultService.GetCharge(charge.Id);
            InvoiceData invoiceData = c.Data.ToObject<InvoiceData>();

            Assert.IsTrue(invoiceData.Number == "123-ABC");
            Assert.IsTrue(invoiceData.AmountInCents == 200);

        }

        [Test]
        public void TestAuthorizeAndVoid()
        {
            Charge charge = defaultService.Authorize(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                CardName = "John Doe",
                CardNumber = "378282246310005",
                CardExpMonth = 12,
                CardExpYear = 2016,
                CardAddress1 = "123 Main Dr",
                CardAddress2 = "Suite 300",
                CardCvv = "123",
                CardPostalCode = "78759",
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == false);

            Charge voided = defaultService.Void(charge.Id, "VOIDREF");

            Assert.IsTrue(voided.Status == "VOIDED");
            Assert.IsTrue(voided.VoidReference == "VOIDREF");
        }

        [Test]
        public void TestAuthorizeAndCapture()
        {
            Charge charge = defaultService.Authorize(new ChargeOptions()
            {
                AmountInCents = 100,
                Currency = "USD",
                Reference = "a new invoice",
                CardName = "John Doe",
                CardNumber = "378282246310005",
                CardExpMonth = 12,
                CardExpYear = 2016,
                CardAddress1 = "123 Main Dr",
                CardAddress2 = "Suite 300",
                CardCvv = "123",
                CardPostalCode = "78759",
                Data = new InvoiceData()
                {
                    Number = "123-ABC",
                    AmountInCents = 200,
                    Date = DateTime.Now
                }
            });
            Assert.IsTrue(charge.Status == "AUTHORIZED");
            Assert.IsTrue(charge.AutoCapture == false);

            Charge captured = defaultService.Capture(charge.Id, 70, "CAPTUREREF");

            Assert.IsTrue(captured.AmountInCents == 70);
            Assert.IsTrue(captured.Status == "SETTLED");
            Assert.IsTrue(captured.CaptureReference == "CAPTUREREF");
        }

        [Test]
        public void TestListCharges()
        {
            SearchResults<Charge> charges = defaultService.Charges();
            Assert.IsTrue(charges.PageSize == 20);
        }

        [Test]
        public void TestSearchCharges()
        {
            //TODO: perform some charges and then search for specific field values
            string rand = new Random().Next().ToString();

            SearchResults<Charge> charges = defaultService.Charges(
                1, //page
                20, //page_size
                "Ref", //search string
                null, // field search string
                DateTime.Now, //from
                DateTime.Now, //to
                null, // accountId
                null //order_by
            );

            Assert.IsTrue(charges.PageSize == 20);
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
