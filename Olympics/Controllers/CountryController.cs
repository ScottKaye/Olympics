using ConsoleGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class CountryController : GenericController<Country, OlympicsContext>
	{
		[MenuItem(Description = "Create a new country")]
		public void _c()
			=> base.Create(s => s.Name, s => s.Population);

		[MenuItem(Description = "Show a list of all countrys")]
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
