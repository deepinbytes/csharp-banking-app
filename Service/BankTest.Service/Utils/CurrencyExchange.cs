using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace BankTest.Service.Utils
{
	public class CurrencyExchange
	{
		public static string Convert(decimal amount, string fromCurrency, string toCurrency)
		{

			//Grab your values and build your Web Request to the API
			string apiURL = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", amount, fromCurrency, toCurrency, Guid.NewGuid().ToString());

			//Make your Web Request and grab the results
			var request = WebRequest.Create(apiURL);

			//Get the Response
			var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

			var result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

			//Get the Result
			return Regex.Replace(result, "[^0-9.]", "");
		}
	}
}