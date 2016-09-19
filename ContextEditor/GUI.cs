using Flicker;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEditor
{
	static class GUI
	{
		static Renderer Renderer { get; set; }
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
			Output.Text += JsonConvert.SerializeObject(obj, new JsonSerializerSettings
			{
				MaxDepth = 1,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				Formatting = Formatting.Indented
			}) + "\n";
		}

		public static void DisplaySet(object[] objs)
		{
			foreach (var o in objs)
			{
				DisplayObject(o);
			}
		}

		public static void DisplaySet(IEnumerable objs)
		{
			foreach (var o in objs)
			{
				DisplayObject(o);
			}
		}

		public static void ClearOutput()
		{
			Output.Text = "";
		}

		public static void Render() => Renderer.Render();
	}
}
