using ConsoleGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class SportController : GenericController<Sport, OlympicsContext>
	{
		[MenuItem(Description = "Create a new sport")]
		public void _c()
			=> base.Create(s => s.Name, s => s.Rules);

		[MenuItem(Description = "Show a list of all sports")]
		public void _r()
			=> base.Read(s => s.Id, s => s.Name, s => s.Rules);

		[MenuItem(Description = "Update an existing sport")]
		public void _u()
			=> base.Update(s => s.Name, s => s.Rules);

		[MenuItem(Description = "Delete an existing sport")]
		public void _d()
			=> base.Delete();
	}
}
