using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestSharp;
using BankServiceTester.BusinessLayer;

namespace BankServiceTester
{
	class Program
	{
		
		static void Main(string[] args)
		{
			
			ShowMenu();
		}

		public static void ShowMenu()
		{
			Console.WriteLine("1. Balance" +
				Environment.NewLine + "2. Deposit" +
				Environment.NewLine + "3. Withdraw" +
				Environment.NewLine + "4.Random Deposit/Withdraw");
			var ans = Console.ReadLine();
			int choice = 0;
			if (int.TryParse(ans, out choice))
			{
				switch (choice)
				{
					case 1:
						GetBalance();
						break;
					case 2:
						Deposit();
						break;
					case 3:
						Withdraw();
						break;
					case 4:
						RandomDepositWithdraw();
						break;
					default:
						Console.WriteLine("Wrong selection!!!" +
							Environment.NewLine + "Press any key for exit");
						Console.ReadKey();
						break;
				}
			}
			else
			{
				Console.WriteLine("You must type numeric value only!!!" +
					Environment.NewLine + "Press any key for exit");
				Console.ReadKey();
			}
		}
		public static void GetBalance()
		{
			Console.WriteLine("Enter accountNumber "+Environment.NewLine);
			var accNumber = Console.ReadLine();

			RestController.getbalance(accNumber);
		
			ShowMenu();

		}

		public static void Deposit()
		{
			Console.WriteLine("Enter accountNumber,currency and amount " + Environment.NewLine);
			var strings = Console.ReadLine();
			string[] values = strings.Split(',');
			RestController.deposit(values[0], values[1], values[2]);
			ShowMenu();
		}

		public static void Withdraw()
		{

			Console.WriteLine("Enter accountNumber,currency and amount " + Environment.NewLine);
			var strings = Console.ReadLine();
			string[] values = strings.Split(',');
			RestController.withdraw(values[0], values[1], values[2]);
			ShowMenu();
		}

		public static void RandomDepositWithdraw()
		{
			Console.WriteLine("Enter accountNumber,currency,amount and number of seconds to run " + Environment.NewLine);
			var strings = Console.ReadLine();
			string[] values = strings.Split(',');

	

			Stopwatch s = new Stopwatch();
			s.Start();
			while (s.Elapsed < TimeSpan.FromSeconds(Convert.ToInt32(values[3])))
			{
				Random rng = new Random();
					var rand=rng.Next(0, 2);
				if (rand == 1)
					RestController.deposit_async(values[0], values[1], values[2]);
				else
					RestController.withdraw_async(values[0], values[1], values[2]);
			}

			s.Stop();

		}
	}
}
