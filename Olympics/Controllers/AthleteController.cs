using ConsoleGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class AthleteController : GenericController<Athlete, OlympicsContext>
	{
		[MenuItem(Description = "Create a new athlete")]
		public void _c()
			=> base.Create(s => s.FirstName, s => s.LastName, s => s.Gender, s => s.Age, s => s.Country_Id);

		[MenuItem(Description = "Show a list of all athletes")]
		public void _r()
			=> base.Read(s => s.Id, s => s.FirstName, s => s.LastName, s => s.Gender, s => s.Age, s => s.Country_Id);

		[MenuItem(Description = "Update an existing athlete")]
		public void _u()
			=> base.Update(s => s.FirstName, s => s.LastName, s => s.Gender, s => s.Age, s => s.Country_Id);

		[MenuItem(Description = "Delete an existing athlete")]
		public void _d()
			=> base.Delete();
	}
}
