﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal;
using PayPal.Manager;
using PayPal.Api.Payments;

namespace RestApiSDKUnitTest
{
    [TestClass()]
    public class CreditCardTest
    {
        private Address GetAddress()
        {
            Address add = new Address();
            add.line1 = "2211";
            add.line2 = "N 1st St";
            add.city = "San Jose";
            add.phone = "408-456-0392";
            add.postal_code = "95131";
            add.state = "California";
            add.country_code = "US";
            return add;
        }

        private CreditCard GetCreditCard()
        {
            CreditCard card = new CreditCard();
            card.cvv2 = "962";
            card.expire_month = 01;
            card.expire_year = 2015;
            card.first_name = "John";
            card.last_name = "Doe";
            card.number = "4825854086744369";
            card.type = "visa";
            card.payer_id = "008";
            card.billing_address = GetAddress();
            return card;
        }

        [TestMethod()]
        public void TestCreditCard()
        {
            CreditCard card = GetCreditCard();
            Address add = GetAddress();
            Assert.AreEqual(card.number, "4825854086744369");
            Assert.AreEqual(card.first_name, "John");
            Assert.AreEqual(card.last_name, "Doe");
            Assert.AreEqual(card.expire_month, 01);
            Assert.AreEqual(card.expire_year, 2015);
            Assert.AreEqual(card.cvv2, "962");
            Assert.AreEqual(card.payer_id, "008");
            Assert.AreEqual(add.city, card.billing_address.city);
            Assert.AreEqual(add.country_code, card.billing_address.country_code);
            Assert.AreEqual(add.line1, card.billing_address.line1);
            Assert.AreEqual(add.line2, card.billing_address.line2);
            Assert.AreEqual(add.phone, card.billing_address.phone);
            Assert.AreEqual(add.postal_code, card.billing_address.postal_code);
            Assert.AreEqual(add.state, card.billing_address.state);
        }

        [TestMethod()]        
        public void ConvertToJsonTest()
        {
            CreditCard card = GetCreditCard();
            string jsonString = card.ConvertToJson();
            CreditCard credit = JsonFormatter.ConvertFromJson<CreditCard>(jsonString);
            Assert.IsNotNull(credit);
        }

        [TestMethod()]
        public void CreditCardGetTest()
        {
            CreditCard card = GetCreditCard();
            CreditCard createdCreditCard = card.Create(UnitTestUtil.GetApiContext());
            CreditCard retrievedCreditCard = CreditCard.Get(UnitTestUtil.GetApiContext(), createdCreditCard.id);
            Assert.AreEqual(createdCreditCard.id, retrievedCreditCard.id);
        }

        [TestMethod()]
        public void CreditCardDeleteTest()
        {
            CreditCard card = GetCreditCard();
            CreditCard createdCreditCard = card.Create(UnitTestUtil.GetApiContext());
            CreditCard retrievedCreditCard = CreditCard.Get(UnitTestUtil.GetApiContext(), createdCreditCard.id);
            retrievedCreditCard.Delete(UnitTestUtil.GetApiContext());
        }

        [TestMethod()]
        public void NullCreditCardIdTest()
        {
            UnitTestUtil.AssertThrownException<System.ArgumentNullException>(() => CreditCard.Get(UnitTestUtil.GetApiContext(), null));
        }
    }
}
