using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BankTest.Service.Model;

namespace BankTest.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IBankService
	{

		
		[WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "balance?accountnumber={accountNumber}")]
		[OperationContract]
		Response Balance(int accountNumber);

		
		[WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "withdraw?accountnumber={accountNumber}&amount={amount}&currency={currency}")]
		[OperationContract]
		Response Withdraw(int accountNumber, decimal amount, string currency);

		
		[WebInvoke(Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "deposit")]
		[OperationContract]
		Response Deposit(int accountNumber, decimal amount, string currency);
	
	}

	
}
