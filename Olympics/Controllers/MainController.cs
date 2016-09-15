using ConsoleGUI;

namespace Olympics.Controllers
{
	class MainController
	{
		[MenuItem(Description = "Countries")]
		public void _c()
		{
			Menu<CountryController>.Load();
		}

		[MenuItem(Description = "Sports")]
		public void _s()
		{
			Menu<SportController>.Load();
		}

		[MenuItem(Description = "Athletes")]
		public void _a()
		{
			Menu<AthleteController>.Load();
		}
	}
}
