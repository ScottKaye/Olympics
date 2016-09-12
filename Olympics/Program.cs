using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Controllers;

namespace Olympics
{
	class Program
	{
		static void Main(string[] args)
		{
			CountryController ct = new CountryController();
			ct.Execute();
		}
	}
}
