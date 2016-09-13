using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGUI
{
	public class MenuItemAttribute : Attribute
	{
		public string Description { get; set; }
	}
}
