using ContextEditor.Editors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ContextEditor
{
	public class ContextManager<TContext> : IDisposable
		where TContext : DbContext, new()
	{
		public TContext Context { get; private set; } = new TContext();
		private ObjectContext Adapter { get; set; }

		public ContextManager()
		{
			Adapter = (Context as IObjectContextAdapter).ObjectContext;
		}

		public void Dispose()
		{
			Context.Dispose();
		}

		/// <summary>
		/// Get all entities that are managed by the context
		/// </summary>
		public IEnumerable<EntityType> GetPOCOs()
		{
			return Adapter.MetadataWorkspace.GetItems(DataSpace.CSpace)
				.Where(i => i.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				.Cast<EntityType>();
		}

		public void Start()
		{
			// Get all types
			var types = GetPOCOs().ToList();

			// Add menu items
			foreach (var entityType in types)
			{
				var clrType = entityType.GetCLRType(Adapter);

				// Dynamically create generic EntityTypeMenu
				var menuItem = typeof(EntityTypeMenu).GetMethod("Create")
					.MakeGenericMethod(new Type[] { clrType })
					.Invoke(null, new object[] { Context, entityType }) as EntityTypeMenu;

				GUI.MainMenu.Items.Add(menuItem);
			}

			// Start rendering (registration closed)
			GUI.Render();
		}
	}
}