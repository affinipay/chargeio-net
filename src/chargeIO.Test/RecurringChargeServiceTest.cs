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
    public class RecurringChargeServiceTest
    {
        RecurringChargeService recurringChargeService;
        PaymentMethodService paymentMethodService;
        string rand;

        [SetUp]
        public void TestInitialize()
        {
            rand = new Random().Next().ToString();
            recurringChargeService = new RecurringChargeService();
            paymentMethodService = new PaymentMethodService();
        }

        [Test]
        public void TestRecurringCharge()
        {
            // Create a recurring charge
            RecurringCharge rc = recurringChargeService.RecurringCharge(new RecurringChargeOptions()
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
                    ExpMonth = 12,
                    ExpYear = 2016
                },
                Schedule = new Schedule()
                {
                    IntervalUnit = "MONTH",
                    IntervalDelay = 1,
                    Start = new DateTime(2016, 1, 1)
                },
                Description = "Test Recurring Charge",
                AmountInCents = 2500
            });

            Assert.NotNull(rc);
            Assert.AreEqual("ACTIVE", rc.Status);
            Assert.NotNull(rc.Id);
            Assert.AreEqual(2016, rc.NextPayment.Year);
            Assert.AreEqual(1, rc.NextPayment.Month);
            Assert.AreEqual(1, rc.NextPayment.Day);


            // Look up the recurring charge by id
            RecurringCharge rc_ = recurringChargeService.GetRecurringCharge(rc.Id);
            Assert.NotNull(rc_.Id);


            // List recurring charges most recent first
            SearchResults<RecurringCharge> rcs = recurringChargeService.RecurringCharges(orderBy: "-created");
            Assert.NotNull(rcs);
            Assert.IsTrue(rcs.TotalEntries >= 1);
            Assert.AreEqual(rc.Id, rcs[0].Id);


            // Get our recurring charge's occurrences
            SearchResults<RecurringChargeOccurrence> occs = recurringChargeService.Occurrences(rc.Id);
            Assert.NotNull(occs);
            Assert.IsTrue(occs.TotalEntries == 1);
            Assert.AreEqual("PENDING", occs[0].Status);
            Assert.AreEqual(2016, occs[0].DueDate.Year);
            Assert.AreEqual(1, occs[0].DueDate.Month);
            Assert.AreEqual(1, occs[0].DueDate.Day);


            // Pre-pay the initial occurrence
            RecurringChargeOccurrence occ = recurringChargeService.PayOccurrence(rc.Id, occs[0].Id);
            Assert.NotNull(occs);
            Assert.AreEqual("PAID", occ.Status);
            Assert.AreEqual(1, occ.Attempts);
            Assert.IsTrue(occ.Charges.Count == 1);
            Assert.AreEqual("AUTHORIZED", occ.Charges[0].Status);


            // Ignore the next pending occurrence
            occs = recurringChargeService.Occurrences(rc.Id);
            Assert.NotNull(occs);
            Assert.IsTrue(occs.TotalEntries == 2);
            Assert.AreEqual("PENDING", occs[0].Status);
            Assert.AreEqual(2016, occs[0].DueDate.Year);
            Assert.AreEqual(2, occs[0].DueDate.Month);
            Assert.AreEqual(1, occs[0].DueDate.Day);

            occ = recurringChargeService.IgnoreOccurrence(rc.Id, occs[0].Id);
            Assert.NotNull(occ);
            Assert.AreEqual("IGNORED", occ.Status);
            Assert.AreEqual(0, occ.Attempts);

            occs = recurringChargeService.Occurrences(rc.Id);
            Assert.NotNull(occs);
            Assert.IsTrue(occs.TotalEntries == 3);
            Assert.AreEqual("PENDING", occs[0].Status);
            Assert.AreEqual(2016, occs[0].DueDate.Year);
            Assert.AreEqual(3, occs[0].DueDate.Month);
            Assert.AreEqual(1, occs[0].DueDate.Day);


            // Update the payment method, amount, and schedule associated with the recurring charge
            List<String> scheduleDays = new List<string>();
            scheduleDays.Add("15");
            rc = recurringChargeService.UpdateRecurringCharge(rc.Id, new RecurringChargeOptions()
            {
                Method = new CardOptions()
                {
                    Name = "FirstName LastName",
                    Address1 = "123 Main St.",
                    Address2 = "Apt #3",
                    City = "Austin",
                    State = "TX",
                    PostalCode = "78759",
                    Number = "4242424242424242",
                    Cvv = "123",
                    ExpMonth = 10,
                    ExpYear = 2018
                },
                Schedule = new Schedule()
                {
                    IntervalUnit = "MONTH",
                    IntervalDelay = 1,
                    Start = new DateTime(2016, 1, 1),
                    Days = scheduleDays
                },
                Description = "Test Recurring Charge",
                AmountInCents = 3000
            });
            Assert.NotNull(rc);
            Assert.AreEqual(2016, rc.NextPayment.Year);
            Assert.AreEqual(2, rc.NextPayment.Month);
            Assert.AreEqual(15, rc.NextPayment.Day);

            occs = recurringChargeService.Occurrences(rc.Id);
            Assert.NotNull(occs);
            Assert.IsTrue(occs.TotalEntries == 3);
            Assert.AreEqual("PENDING", occs[0].Status);
            Assert.AreEqual(2016, occs[0].DueDate.Year);
            Assert.AreEqual(2, occs[0].DueDate.Month);
            Assert.AreEqual(15, occs[0].DueDate.Day);


            // Pay the ignored occurrence with the updated payment method
            occ = recurringChargeService.PayOccurrence(rc.Id, occs[0].Id);
            Assert.NotNull(occs);
            Assert.AreEqual("PAID", occ.Status);
            Assert.AreEqual(1, occ.Attempts);
            Assert.IsTrue(occ.Charges.Count == 1);
            Assert.AreEqual("AUTHORIZED", occ.Charges[0].Status);
            Assert.AreEqual("************4242", ((Card)occ.Charges[0].PaymentMethod).Number);


            // Cancel the recurring charge
            rc = recurringChargeService.CancelRecurringCharge(rc.Id);
            Assert.NotNull(rc);
            Assert.AreEqual("COMPLETED", rc.Status);
            Assert.AreEqual("user_canceled", rc.StatusReason);


            // Delete the recurring charge
            rc = recurringChargeService.DeleteRecurringCharge(rc.Id);
            Assert.NotNull(rc);
            Assert.AreEqual("DELETED", rc.Status);

        }
    }
}
