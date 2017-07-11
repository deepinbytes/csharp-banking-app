using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using BankTest.Service;

namespace BankTest.UnitTests
{

		[TestFixture]
		internal class ServiceTests
		{

			private static BankService _myService;
			private IBankService _myIservice;
			[OneTimeSetUp]
			public void SetUp()
			{
				_myIservice = MockRepository.GenerateMock<IBankService>();
				_myService = new BankService(_myIservice);
			}

		[Test(Description = "To Check Balance Service")]
		[TestCase(10002313, 20)]
		[TestCase(10001232, 1000000)]
		[TestCase(10003478, 0)]
		[TestCase(1000347, 0)]
		public void GetBalance(int accNumber,int expected)
		{
			//Set Up  
			var crs = _myService.Balance(accNumber);
			//Assert  
			if (accNumber == 1000347)
			{
				Assert.AreEqual("Transaction - Balance: Failed. Error:-Account Number not found", crs.Message, "Account Error");
			}
			else
			{
				Assert.IsNotNull(crs.Balance, "The returned value is null");
				Assert.AreEqual(expected, crs.Balance, "Incorrect Balance");
			}
		}

		[Test(Description = "To Check Deposit Service")]
		[TestCase(10003452, "SGD",20, "Transaction - Deposit: Successful.")]
		[TestCase(10003467, "SGD",200, "Transaction - Deposit: Successful.")]
		[TestCase(10003467, "EUR", 200, "Transaction - Deposit: Successful.")]//Remittance account
		[TestCase(10003452, "USD",20, "Transaction - Deposit: Failed. Error:-Currency type mismatch.")] //Non remittance account
		public void Deposit(int accNumber,string currency,decimal amount,string expected)
		{
			//Set Up  
			var crs = _myService.Deposit(accNumber,amount,currency);
			//Assert  

			Assert.IsNotNull(crs.Message, "The returned value is null");
			Assert.AreEqual(expected, crs.Message, "Incorrect Balance");
		}


		[Test(Description = "To Check Deposit Service")]
		[TestCase(10003452, "SGD", 20, "Transaction - Withdraw: Successful.")]
		[TestCase(10003467, "SGD", 200, "Transaction - Withdraw: Successful.")]
		[TestCase(10003467, "EUR", 2, "Transaction - Withdraw: Successful.")]//Remittance account
		[TestCase(10003452, "USD", 20, "Transaction - Withdraw: Failed. Error:-Currency type mismatch.")]
		[TestCase(10003478, "INR", 20, "Transaction - Withdraw: Failed. Error:-Overdraft Not Accepted.")]
		//Non remittance account
		public void Withdraw(int accNumber, string currency, decimal amount, string expected)
		{
			//Set Up  
			var crs = _myService.Withdraw(accNumber, amount, currency);
			//Assert  

			Assert.IsNotNull(crs.Message, "The returned value is null");
			Assert.AreEqual(expected, crs.Message, "Incorrect Balance");
		}
	}
	
}
