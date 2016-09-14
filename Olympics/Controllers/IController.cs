using System;
using System.Linq.Expressions;

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
