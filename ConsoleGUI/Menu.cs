using Common;
using System;
using System.Linq;

namespace ConsoleGUI
{
	public static class Menu
	{
		public static void LoadMenu<TMenu>()
			where TMenu : class, new()
		{
			var menu = new TMenu();

			// Display menu items
			var methods = menu.GetType().GetMethods()
				.Where(m => m.GetCustomAttributes(typeof(MenuItemAttribute), false).Length > 0)
				.ToList();

			// TODO Console.Table
			for (int i = 0; i < methods.Count(); ++i)
			{
				string name = methods[i].GetAttribute<MenuItemAttribute>()?.Description ?? methods[i].Name;
				Console.WriteLine($"{i + 1} - {name}");
			}

			// Handle input
			Console.Write("Choose a menu item: ");
			int index;
			if (!int.TryParse(Console.ReadLine(), out index))
			{
				Console.WriteLine("Unexpected input.");
				return;
			}

			--index;

			methods[index].Invoke(menu, null);
		}
	}
}
