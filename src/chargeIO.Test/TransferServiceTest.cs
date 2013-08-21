using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChargeIO;

namespace ChargeIO.Test
{
    [TestFixture]
    public class TransferServiceTest
    {
        TransferService defaultTransferService;
        TransferOptions defaultTransferOptions;
        string rand;

        [SetUp]
        public void TestInitialize()
        {
            defaultTransferOptions = new TransferOptions()
            {
                AmountInCents = 400,
                Type = "DEBIT",
                //Currency = "USD",
                BankAccountNumber = "1100000002",
                BankAccountType = "CHECKING",
                BankRoutingNumber = "111000025",
                Name = "An ACH Customer",
                Address1 = "123 Main St.",
                Address2 = "Apt #3",
                City = "Austin",
                State = "TX",
                PostalCode = "78759",
                Country = "US"
            };
            
            rand = new Random().Next().ToString();
            defaultTransferService = new TransferService();
        }

       
        [Test]
        public void TestTransfer()
        {
            Transfer t = defaultTransferService.Create(defaultTransferOptions);
            Assert.IsTrue(t.AmountInCents == 400);
        }
        
        [Test]
        public void TestGetTransfer()
        {
            Transfer t = defaultTransferService.Create(defaultTransferOptions);
            Assert.IsTrue(t.AmountInCents == 400);
            Transfer t1 = defaultTransferService.Get(t.Id);
            Assert.IsTrue(t1.AmountInCents == 400);
        }

        [Test]
        public void TestCancelTransfer()
        {
            Transfer t = defaultTransferService.Create(defaultTransferOptions);
            Assert.IsTrue(t.AmountInCents == 400);
            Transfer cancelled = defaultTransferService.Cancel(t.Id);
            Assert.IsTrue(cancelled.AmountInCents == 400);
        }

        [Test]
        public void TestListTransfers()
        {
            SearchResults<Transfer> transfers = defaultTransferService.List();
            Assert.IsTrue(transfers.Page == 1);
        }
    }
}
