using Flicker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ContextEditor
{
	internal static class GUI
	{
		private static Renderer Renderer { get; set; }
		public static MenuElement MainMenu { get; set; }
		public static MenuElement SubMenu { get; set; }
		public static MenuElement NavPropMenu { get; set; }
		public static TextElement Output { get; set; }

		static GUI()
		{
			Renderer = new Renderer();

			MainMenu = new MenuElement(0, 0, 20, 6)
			{
				Border = '=',
				Background = ConsoleColor.DarkBlue
			};

			SubMenu = new MenuElement(20, 0, 30, 6)
			{
				Visible = false
			};

			NavPropMenu = new MenuElement(50, 0, Console.BufferWidth - 50, 6)
			{
				Visible = false,
				Background = ConsoleColor.DarkRed
			};

			Output = new TextElement(0, 6, Console.BufferWidth, Console.BufferHeight - 7)
			{
				Border = '-'
			};

			Renderer.Register(MainMenu);
			Renderer.Register(SubMenu);
			Renderer.Register(NavPropMenu);
			Renderer.Register(Output);
		}

		public static void DisplayObject(object obj)
		{
			Output.Text += string.Join(", ", obj.GetType().GetPrimitiveProperties().Select(p => $"{p.Name}: {p.GetValue(obj)}"));
			Output.Text += "\n";
		}

		public static void DisplaySet(IEnumerable<object> objs)
		{
			if (!objs.Any())
			{
				Output.Text += "Empty set";
				return;
			}

			var props = objs.First().GetType().GetPrimitiveProperties();

			foreach (var o in objs)
			{
				var output = new List<string>();

				foreach (var prop in props)
				{
					var value = o.GetType().GetProperty(prop.Name).GetValue(o).ToString();
					var longestLength = objs.Select(k => k.GetType().GetProperty(prop.Name).GetValue(k).ToString().Length).Max();
					output.Add(value.PadRight(longestLength));
				}

				Output.Text += string.Join(" \u2502 ", output);
				Output.Text += "\n";
			}
		}

		public static void DisplaySet(object[] objs)
		{
			DisplaySet(objs.AsEnumerable());
		}

		public static void ClearOutput()
		{
			Output.Text = "";
		}

		public static void Render() => Renderer.Render();
	}
}