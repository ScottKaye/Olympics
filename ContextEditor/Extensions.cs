using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ContextEditor
{
	internal static class Extensions
	{
		/// <summary>
		/// Resolve an EntityType to a CLR type.  Since the CLR types are internal to EF, we have to check the namespace directly...
		/// </summary>
		/// Author: http://stackoverflow.com/a/24128797/382456
		/// <param name="type">Entity type to resolve</param>
		/// <returns>Resolved entity type</returns>
		internal static Type GetCLRType(this EntityType type, ObjectContext adapter)
		{
			return (Type)
				adapter.MetadataWorkspace
				.GetItems<EntityType>(DataSpace.CSpace)
				.Single(s => s.FullName == type.FullName)
				.MetadataProperties
				.Single(p => p.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType")
				.Value;
		}

		internal static Type GetCLRType(this EntityType type, DbContext context)
		{
			return type.GetCLRType((context as IObjectContextAdapter).ObjectContext);
		}

		internal static IEnumerable<PropertyInfo> GetNavProps(this Type type)
		{
			return type.GetProperties()
				.Where(p => p.GetGetMethod().IsVirtual)
				.Where(p => p.PropertyType.GetInterface("IEnumerable") != null);
		}

		internal static string Singularize(this string str)
		{
			var service = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
			return service.Singularize(str);
		}

		internal static string Pluralize(this string str)
		{
			var service = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
			return service.Pluralize(str);
		}

		internal static IEnumerable<PropertyInfo> GetPrimitiveProperties(this Type type)
		{
			return type.GetProperties()
				.Where(p => p.PropertyType.IsPrimitive || p.PropertyType == typeof(string));
		}
	}
}