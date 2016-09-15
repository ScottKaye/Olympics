using ConsoleGUI;
using ContextEditor;
using ContextEditor.Editors;
using Olympics.Controllers;
using Olympics.Models;
using System;

namespace Olympics
{
	class Program
	{
		static void Main(string[] args)
		{
			/*var reader = new Reader<OlympicsContext>();
			var types = reader.GetPOCOs();
			var editor = new CLI(types);
			editor.Start();

			Console.ReadKey();
			return;*/

			Console.WriteLine("Welcome to the Olympics!");

			Menu<MainController>.Load();
		}
	}
}
