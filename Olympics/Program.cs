using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Controllers;
using ConsoleGUI;

namespace Olympics
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the Olympics!");

			while (true)
			{
				Menu.LoadMenu<MainController>();
			}

			Console.ReadKey();
		}
	}
}
