using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	interface IController<T>
	{
		void Create(params Expression<Func<T, object>>[] editableProps);
		void Read(params Expression<Func<T, object>>[] viewableProps);
		void Update(params Expression<Func<T, object>>[] editableProps);
		void Delete();
	}
}
