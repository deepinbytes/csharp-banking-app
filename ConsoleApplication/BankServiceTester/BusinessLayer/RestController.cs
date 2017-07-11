using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace BankServiceTester.BusinessLayer
{
	class RestController
	{
		private const string url = "http://localhost:55038";

		public static void withdraw(string accNumber, string currency, string amount)
		{
			try
			{
				var client = new RestClient(url);
				var request = new RestRequest("/BankService.svc/withdraw?acc=" + accNumber + "&currency=" + currency + "&amount=" + amount, Method.GET);
				var response = client.Execute(request);
				Console.WriteLine(response.Content + Environment.NewLine);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		public static void deposit(string accNumber, string currency, string amount)
		{
			try
			{
				var client = new RestClient(url);
				var request = new RestRequest("/BankService.svc/deposit", Method.PUT);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(new
				{
					accNumber = accNumber,
					currency = currency,
					amount = amount
				});

				var response = client.Execute(request);
				Console.WriteLine(response.Content + Environment.NewLine);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


		public static void withdraw_async(string accNumber, string currency, string amount)
		{
			try
			{
				var client = new RestClient(url);
				var request = new RestRequest("/BankService.svc/withdraw?acc=" + accNumber + "&currency=" + currency + "&amount=" + amount, Method.GET);
				client.ExecuteAsync(request, response => {
					Console.WriteLine(response.Content + Environment.NewLine);
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		public static void deposit_async(string accNumber, string currency, string amount)
		{
			try
			{
				var client = new RestClient(url);
				var request = new RestRequest("/BankService.svc/deposit", Method.PUT);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(new
				{
					accNumber = accNumber,
					currency = currency,
					amount = amount
				});

				client.ExecuteAsync(request, response => {
					Console.WriteLine(response.Content + Environment.NewLine);
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static void getbalance(string accNumber)
		{
			try
			{
			var client = new RestClient(url);
			var request = new RestRequest("/BankService.svc/balance?acc=" + accNumber, Method.GET);
			var response = client.Execute(request);
			Console.WriteLine(response.Content + Environment.NewLine);
		}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
}

	}
}
