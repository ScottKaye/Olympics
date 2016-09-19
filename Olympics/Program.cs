using ConsoleGUI;
using ContextEditor;
using ContextEditor.Editors;
using Flicker;
using Olympics.Controllers;
using Olympics.Models;
using System;
using System.Linq;

namespace Olympics
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var manager = new ContextManager<OlympicsContext>())
			{
				manager.Start();
			}
		}
	}
}
