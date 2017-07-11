using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BankTest.Service.Enum;

namespace BankTest.Service.DAL
{
	public class BankDAL 
	{
	
		public Account Balance(int accNumber)
		{
			using (var bankDBContext = new TestBankDBEntities())
			{
				// Get Account Details
				
				var acc = bankDBContext.Accounts.SingleOrDefault(x => x.Id == accNumber);
				if (acc == null)
				{
					throw new ApplicationException("Account Number not found");
				}
				return acc;
			}
		}


	
		public Account Deposit(int accNumber, decimal amount, string currency)
		{
			using (var bankDBContext = new TestBankDBEntities())
			{
				try
				{
					var acc = ValidateAccountandCurrency(accNumber, bankDBContext, currency);


					// Adding The transaction
					var transactionType = bankDBContext.TransactionTypes.SingleOrDefault(x => x.Type == TransactionTypes.Deposit.ToString());

					// Create an entry in Transaction table
					var transaction = bankDBContext.Transactions.Add(new Transaction()
					{
						AccountId = acc.Id,
						Amount = amount,
						RequestCurrency=currency,
						CurrentBalance = acc.Balance,
						TransactionType = transactionType.Id,
						TransactionDate = DateTime.Now,

					});

					acc.Balance += amount;
					acc.Currency = currency;
					bankDBContext.SaveChanges();
					if (transaction.Id > 0)
					{
						return acc;
					}
					else
					{
						throw new ApplicationException("Transaction failed.");
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}



		public Account Withdraw(int accNumber, decimal amount, string currency)
		{
			using (var bankdbContext = new TestBankDBEntities())
			{
				try
				{
					// Get Account Details
					var acc = ValidateAccountandCurrency(accNumber, bankdbContext, currency);


					var transactionType = bankdbContext.TransactionTypes.SingleOrDefault(x => x.Type == TransactionTypes.Withdraw.ToString());

					if (acc.Balance > 0 && acc.Balance > amount)
					{

						var transaction = bankdbContext.Transactions.Add(new Transaction()
						{
							AccountId = acc.Id,
							Amount = amount,
							RequestCurrency=currency,
							CurrentBalance = acc.Balance,
							TransactionType = transactionType.Id,
							TransactionDate = DateTime.Now,

						});

						acc.Balance -= amount;
						acc.Currency = currency;
						bankdbContext.SaveChanges();
						if (transaction.Id > 0)
						{
							return acc;
						}
						else
						{
							throw new ApplicationException("Transaction failed.");
						}
					}
					else
					{
						throw new ApplicationException("Overdraft Not Accepted.");
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}

			}
		}


		private Account ValidateAccountandCurrency(int accNumber, TestBankDBEntities bankDBContext, string currency)
		{
			var account = bankDBContext.Accounts.SingleOrDefault(x => x.Id == accNumber);
			if (account == null)
			{
				throw new ApplicationException("Account number not found.");
			}
			else if (account.Currency != currency)
			{
				throw new ApplicationException("Currency type mismatch.");
			}
			else
				return account;
		}
	}
}
