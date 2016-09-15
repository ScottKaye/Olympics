using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEditor
{
	public class Reader<TContext>
		where TContext : DbContext, new()
	{
		/// <summary>
		/// Get all entities that are managed by the context
		/// </summary>
		public IEnumerable<EntityType> GetPOCOs()
		{
			var context = (new TContext() as IObjectContextAdapter).ObjectContext;
			return context.MetadataWorkspace.GetItems(DataSpace.CSpace)
				.Where(i => i.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				.Cast<EntityType>();
		}
	}
}
