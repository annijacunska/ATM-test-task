using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashMachine;
using System.IO;
using System;

namespace ATMTests
{
    [TestClass]
    public class ATMClassTests
    {

        ATM atm = new ATM();

        /* Only Insert and Withdraw methods were tested, assuming that methods RequestBanknotes and RequestAmount 
         * have already validated the input data to be a positive int for Withdraw() and int[] for Insert()
         * */

        [TestMethod]
        public void Deposit_Invalid_Banknotes()
        {
            string expected = "Deposit only accepts 5, 10, 20, 50 and 100 banknotes";
            string actual;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            int[] cashInput = { 1, 5, 4 };
            atm.Insert(cashInput);

            actual = stringWriter.ToString();

            StringAssert.StartsWith(actual, expected);
        }

        [TestMethod]
        public void Deposit_Valid_Banknotes()
        {
            string expected = "DEPOSIT SUCCESSFUL";
            string actual;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            int[] cashInput = { 10, 5, 10 };
            atm.Insert(cashInput);

            actual = stringWriter.ToString();

            StringAssert.StartsWith(actual, expected);
            Assert.AreEqual(25, atm.Balance);
        }


        [TestMethod]
            [DataRow(160, "Insufficient funds", false)]
            [DataRow(100, "Unable to withdraw this amount", false)]
            [DataRow(30, "WITHDRAWAL SUCCESSFUL", true)]
        public void Withdraw_Amount(int amount, string expected, bool success)
        {
            string actual;
            atm.Balance = 150;

            //Amount of banknote denominations
            atm.banknoteAmount.Add("5", 3);
            atm.banknoteAmount.Add("10", 0);
            atm.banknoteAmount.Add("20", 1);
            atm.banknoteAmount.Add("50", 1);
            atm.banknoteAmount.Add("100", 0);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            atm.Withdraw(amount);

            actual = stringWriter.ToString();

            StringAssert.StartsWith(actual, expected);
            if(success)
            {
                Assert.AreEqual(150 - amount, atm.Balance);
            }
        }
    }
}