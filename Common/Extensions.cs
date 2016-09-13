using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
		public static MemberInfo Member<T, U>(this Expression<Func<T, U>> exp)
			=> (((exp.Body as UnaryExpression)?.Operand ?? exp.Body) as MemberExpression).Member;

		public static T GetAttribute<T>(this MethodInfo method)
			where T : class => method.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
	}
}
