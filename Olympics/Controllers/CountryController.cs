using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class CountryController
	{
		public void Execute()
		{
			char command = '_';

			do
			{
				switch (command)
				{
					case 'C':
						Create();
						break;
					case 'R':
						Read();
						break;
					case 'U':
						Update();
						break;
					case 'D':
						Delete();
						break;
				}

				Console.Write("What do you want to do? [C, R, U, D, X]: ");
			}
			while ((command = Console.ReadLine().ToUpperInvariant().FirstOrDefault()) != 'X');
		}

		void Create()
		{
			string name;
			int population;

			Console.Write("Name: ");
			name = Console.ReadLine();
			Console.Write("Population: ");
			int.TryParse(Console.ReadLine(), out population);

			using (var db = new OlympicsContext())
			{
				db.Countries.Add(new Country
				{
					Name = name,
					Population = population
				});

				db.SaveChanges();
			}
		}

		void Read()
		{
			using (var db = new OlympicsContext())
			{
				foreach (var c in from c in db.Countries
								  orderby new { c.Population, c.Name } descending
								  select c)
				{
					Console.WriteLine("{0,4} | {1,-15} | {2,12}", c.Id, c.Name, c.Population);
				}
			}
		}

		void Update()
		{
			int id;
			string name;
			int population;

			Console.Write("What ID? ");
			int.TryParse(Console.ReadLine(), out id);

			using (var db = new OlympicsContext())
			{
				Country country = db.Countries.FirstOrDefault(c => c.Id == id);
				if (country == null)
				{
					throw new ArgumentNullException();
				}

				Console.Write($"New name for {country.Name}: ");
				name = Console.ReadLine();
				Console.Write($"New population for {country.Name}: ");
				int.TryParse(Console.ReadLine(), out population);

				country.Name = name;
				country.Population = population;
				db.SaveChanges();
			}
		}

		void Delete()
		{
			int id;

			Console.Write("What ID? ");
			int.TryParse(Console.ReadLine(), out id);

			using (var db = new OlympicsContext())
			{
				Country country = db.Countries.FirstOrDefault(c => c.Id == id);
				db.Countries.Remove(country);
				db.SaveChanges();
			}
		}
	}
}
