using ConsoleGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class MainController
	{
		[MenuItem(Description = "Countries")]
		public void _c()
		{
			Menu.LoadMenu<CountryController>();
		}

		[MenuItem(Description = "Sports")]
		public void _s()
		{
			Menu.LoadMenu<SportController>();
		}

		[MenuItem(Description = "Athletes")]
		public void _a()
		{
			Menu.LoadMenu<AthleteController>();
		}
	}
}
