using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BankTest.Service.Model
{
	[DataContract]
	public class Response
	{
		[DataMember]
		public int AccountNumber { get; set; }

		[DataMember]
		public bool Successful { get; set; }

		[DataMember]
		public decimal Balance { get; set; }

		[DataMember]
		public string Currency { get; set; }

		[DataMember]
		public string Message { get; set; }
	}
}