using System;
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
