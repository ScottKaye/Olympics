﻿using System;
using System.Collections.Generic;
using System.Linq;
using Flicker;

namespace ContextEditor
{
	internal static class GUI
	{
		static GUI()
		{
			Renderer = new Renderer();

			MainMenu = new MenuElement(0, 0, .33f, .4f)
			{
				Background = ConsoleColor.DarkBlue
			};

			SubMenu = new MenuElement(.33f, 0, .33f, .4f)
			{
				Visible = false
			};

			NavPropMenu = new MenuElement(.66f, 0, .33f, .4f)
			{
				Visible = false,
				Background = ConsoleColor.DarkRed
			};

			Output = new TextElement(0, .4f, 1, .6f)
			{
				Border = '-'
			};

			Renderer.Register(MainMenu);
			Renderer.Register(SubMenu);
			Renderer.Register(NavPropMenu);
			Renderer.Register(Output);
		}

		private static Renderer Renderer { get; }
		public static MenuElement MainMenu { get; set; }
		public static MenuElement SubMenu { get; set; }
		public static MenuElement NavPropMenu { get; set; }
		public static TextElement Output { get; set; }

		public static void DisplayObject(object obj)
		{
			Output.Text += string.Join(", ", obj.GetType().GetPrimitiveProperties().Select(p => $"{p.Name}: {p.GetValue(obj)}"));
			Output.Text += "\n";
		}

		public static void DisplaySet(ICollection<object> objs)
		{
			if (!objs.Any())
			{
				Output.Text += "Empty set";
				return;
			}

			var props = objs.First().GetType().GetPrimitiveProperties().ToList();

			foreach (var o in objs)
			{
				var output = (from prop in props
							  let value = o.GetType().GetProperty(prop.Name).GetValue(o).ToString()
							  let longestLength = objs.Select(k => k.GetType().GetProperty(prop.Name).GetValue(k).ToString().Length).Max()
							  select value.PadRight(longestLength)).ToList();

				Output.Text += string.Join(" \u2502 ", output);
				Output.Text += "\n";
			}
		}

		public static void ClearOutput()
		{
			Output.Text = "";
		}

		public static void Render() => Renderer.Render();
	}
}