using ConsoleGUI;
using Olympics.Models;

namespace Olympics.Controllers
{
	class CountryController : GenericController<Country, OlympicsContext>
	{
		[MenuItem(Description = "Create a new country")]
		public void _c()
			=> base.Create(s => s.Name, s => s.Population);

		[MenuItem(Description = "Show a list of all countries")]
		public void _r()
			=> base.Read(s => s.Id, s => s.Name, s => s.Population);

		[MenuItem(Description = "Update an existing country")]
		public void _u()
			=> base.Update(s => s.Name, s => s.Population);

		[MenuItem(Description = "Delete an existing country")]
		public void _d()
			=> base.Delete();
	}
}
