using Common;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace ConsoleGUI
{
	public static class Menu<TMenu>
		where TMenu : class, new()
	{
		public static void Load()
		{
			// Display menu items
			var methods = typeof(TMenu).GetMethods()
				.Where(m => m.GetCustomAttributes(typeof(MenuItemAttribute), false).Length > 0)
				.ToList();

			DrawMenu(methods);
		}

		private static string GetName(MethodInfo method)
		{
			return method.GetAttribute<MenuItemAttribute>()?.Description ?? method.Name;
		}

		private static void DrawMenu(List<MethodInfo> methods, int highlightedIndex = 0)
		{
			Console.Clear();
			if (methods.Count == 0)
			{
				Console.WriteLine("No methods to list");
				return;
			}

			for (int i = 0; i < methods.Count; ++i)
			{
				if (i == highlightedIndex)
				{
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.ForegroundColor = ConsoleColor.Black;
				}

				Console.WriteLine($"{GetName(methods[i])}");
				Console.ResetColor();
			}

			// Wait for input
			var c = Console.ReadKey(false).Key;

			switch (c)
			{
				case ConsoleKey.Enter:
					methods[highlightedIndex].Invoke(new TMenu(), null);
					break;
				case ConsoleKey.DownArrow:
					++highlightedIndex;
					break;
				case ConsoleKey.UpArrow:
					--highlightedIndex;
					break;
			}

			DrawMenu(methods, highlightedIndex.Wrap(0, methods.Count - 1));
		}
	}
}
