using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week2_modelfirst.Controllers;

namespace week2_modelfirst
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
