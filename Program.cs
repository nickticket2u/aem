using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace aem4
{
	internal class Program
	{
		static string token;
		static void Login(Action action)
		{
			System.Threading.Tasks.Task.Run(async () =>
			{
				Console.WriteLine("Getting token\n");
				try
				{
				
					token = await data.helper.Login(
						ConfigurationManager.AppSettings["username"],
						ConfigurationManager.AppSettings["password"]);
					Console.WriteLine($"Returned token {token}\n");
					if(action != null)
					{
						action();
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
				
			});
		}

		static void GetData()
		{
			System.Threading.Tasks.Task.Run(async () =>
			{
				Console.WriteLine("Getting Data\n");
				try
				{
					data.helper.GetData(token,(s)=>Console.WriteLine(s));
				}catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			});
		}

		static void Main(string[] args)
		{
			Console.WriteLine(@"
Hello World.
Press a to sync data
Press q to quit
			");

			

			while (true)
			{
				string key = Console.ReadLine();
				if (key == "a")
				{
					if (token == null)
					{
						Login(GetData);
					}
					else
					{
						GetData();
					}
				}
				else if(key == "q")
				{
					Console.WriteLine("Goodbye");
					return;
				}
			}
		}
	}
}
