using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BankTest.Service.Model;
using BankTest.Service.DAL;
using BankTest.Service.Enum;
using BankTest.Service.Utils;

namespace BankTest.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class BankService : IBankService
	{
		#region constants
		private const string TRANSACTION_WITHDRAW_SUCCESS = "Transaction - Withdraw: Successful.";
		private const string TRANSACTION_WITHDRAW_FAILURE = "Transaction - Withdraw: Failed. Error:-";
		private const string TRANSACTION_DEPOSIT_SUCCESS = "Transaction - Deposit: Successful.";
		private const string TRANSACTION_DEPOSIT_FAILURE = "Transaction - Deposit: Failed. Error:-";
		private const string TRANSACTION_BALANCE_SUCCESS = "Transaction - Balance: Successful.";
		private const string TRANSACTION_BALANCE_FAILURE = "Transaction - Balance: Failed. Error:-";
		#endregion
		private static BankDAL _myDALContext;
		private static IBankService _MyIService;


		
				public BankService(IBankService _myIService)
				{
					_myDALContext =new BankDAL();
					_MyIService = _myIService;
				}

		public BankService()
		{
			_myDALContext = new BankDAL();
		}

		public Response Balance(int accNumber)
		{
		
			try
			{
				var account = _myDALContext.Balance(accNumber);
				Response result = new Response()
				{
					AccountNumber = account.Id,
					Successful = true,
					Balance = account.Balance,
					Currency = account.Currency,
					Message = TRANSACTION_BALANCE_SUCCESS
				};
				return result;
			}
			catch (Exception ex)
			{
				return new Response()
				{
					AccountNumber = accNumber,
					Successful = false,
					Message =  TRANSACTION_BALANCE_FAILURE+ ex.Message,
					
				};
			}
		}

		public Response Deposit(int accNumber, decimal amount, string currency)
		{
			try
			{
				
				Account account=null;
				var check_account_type = _myDALContext.Balance(accNumber);
				if (check_account_type.AccountType == (Int32)AccountTypes.RemittanceAccount)
				{
					account = _myDALContext.Deposit(accNumber, Convert.ToDecimal(CurrencyExchange.Convert(amount, currency.ToUpper(), check_account_type.Currency.ToUpper())), check_account_type.Currency.ToUpper());
				}
				else
				{
					account = _myDALContext.Deposit(accNumber, amount, currency);
				}

				Response result=new Response()
				{
					AccountNumber = account.Id,
					Successful = true,
					Balance = account.Balance,
					Currency = account.Currency,
					Message = TRANSACTION_DEPOSIT_SUCCESS
				};
				return result;
			} catch (Exception ex)
			{
				return new Response()
				{
					AccountNumber = accNumber,
					Successful = false,
					Message = TRANSACTION_DEPOSIT_FAILURE+ ex.Message,
				};
			}
		}

		public Response Withdraw(int accNumber, decimal amount, string currency)
		{
			try
			{
				
				Account account = null;
				var check_account_type = _myDALContext.Balance(accNumber);

				if (check_account_type.AccountType == (Int32)AccountTypes.RemittanceAccount)
				{
					account = _myDALContext.Withdraw(accNumber, Convert.ToDecimal(CurrencyExchange.Convert(amount, currency.ToUpper(), check_account_type.Currency.ToUpper())), check_account_type.Currency.ToUpper());
				}
				else
				{

					account = _myDALContext.Withdraw(accNumber, amount, currency);
				}
				 Response result=new Response()
				{
					AccountNumber = account.Id,
					Successful = true,
					Balance = account.Balance,
					Currency = account.Currency,
					Message = TRANSACTION_WITHDRAW_SUCCESS,
				 };
				return result;
			}
			catch (Exception ex)
			{
				
					return new Response()
				{
					AccountNumber = accNumber,
					Successful = false,
					Message = TRANSACTION_WITHDRAW_FAILURE + ex.Message
					};
			}

		}
	}
}
