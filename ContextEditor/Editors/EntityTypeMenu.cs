using Flicker;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace ContextEditor.Editors
{
	internal class EntityTypeMenu : MenuItem
	{
		private DbContext Context { get; set; }
		private EntityType Type { get; set; }

		private EntityTypeMenu(DbContext context, EntityType type)
		{
			Context = context;
			Type = type;

			Label = type.Name.Singularize();
		}

		public static EntityTypeMenu Create<TEntity>(DbContext context, EntityType type)
			where TEntity : class, new()
		{
			return new EntityTypeMenu(context, type)
			{
				Method = () => CreateSubMenu<TEntity>(context, type)
			};
		}

		private static void CreateSubMenu<TEntity>(DbContext context, EntityType type)
			where TEntity : class, new()
		{
			var rawType = type.GetCLRType(context);

			GUI.SubMenu.Items.Clear();

			// Add CRUD actions for this type to sub menu
			GUI.SubMenu.Items.Add(new MenuItem
			{
				Label = $"Create new {type.Name.Singularize()}",
				Method = () =>
				{
					var newEntity = new TEntity();

					foreach (var primitiveProp in rawType.GetPrimitiveProperties()
						.Where(p => !type.KeyProperties.Select(k => k.Name).Contains(p.Name)))
					{
						string input;
						new InputElement(out input, $"{primitiveProp.Name}: ");
						var casted = Convert.ChangeType(input, primitiveProp.PropertyType);
						typeof(TEntity).GetProperty(primitiveProp.Name).SetValue(newEntity, casted);
					}

					context.Set<TEntity>().Add(newEntity);
					context.SaveChanges();
				}
			});

			GUI.SubMenu.Items.Add(new MenuItem
			{
				Label = $"List all {type.Name.Pluralize()}",
				Method = () =>
				{
					GUI.ClearOutput();
					GUI.DisplaySet(context.Set<TEntity>());
				}
			});

			GUI.SubMenu.Items.Add(new MenuItem
			{
				Label = $"Update existing {type.Name.Singularize()}",
				Method = () =>
				{
					int baseId;
					new InputElement<int>(out baseId, $"{type.Name.Singularize()} ID: ");
					var baseEntity = context.Set<TEntity>().Find(baseId);

					foreach (var primitiveProp in rawType.GetPrimitiveProperties()
						.Where(p => !type.KeyProperties.Select(k => k.Name).Contains(p.Name)))
					{
						var currentValue = primitiveProp.GetValue(baseEntity);
						string input;
						new InputElement(out input, $"{primitiveProp.Name} (default {currentValue}): ");

						if (input.Length > 0)
						{
							var casted = Convert.ChangeType(input, primitiveProp.PropertyType);
							typeof(TEntity).GetProperty(primitiveProp.Name).SetValue(baseEntity, casted);
						}
					}

					context.SaveChanges();
				}
			});

			GUI.SubMenu.Items.Add(new MenuItem
			{
				Label = $"Delete existing {type.Name.Singularize()}",
				Method = () =>
				{
					int baseId;
					new InputElement<int>(out baseId, $"{type.Name.Singularize()} ID: ");
					var baseEntity = context.Set<TEntity>().Find(baseId);

					context.Set<TEntity>().Remove(baseEntity);
					context.SaveChanges();
				}
			});

			GUI.SubMenu.Visible = true;
			GUI.SubMenu.Select();

			// Add navigation props, if they exist
			var navProps = typeof(TEntity).GetNavProps().ToList();
			if (navProps.Any())
			{
				GUI.NavPropMenu.Visible = true;

				foreach (var prop in navProps)
				{
					var navProp = typeof(TEntity).GetProperty(prop.Name);

					dynamic navSet = context.GetType().GetMethods()
						.Where(m => m.IsGenericMethod)
						.Single(m => m.Name == "Set")
						.MakeGenericMethod(new Type[] { prop.PropertyType.GenericTypeArguments[0] })
						.Invoke(context, null);

					GUI.NavPropMenu.Items.Clear();
					GUI.NavPropMenu.Items.Add(new MenuItem
					{
						Label = prop.Name,
						Method = () =>
						{
							GUI.SubMenu.Items.Clear();

							// Add CRUD actions for this type to sub menu
							GUI.SubMenu.Items.Add(new MenuItem
							{
								Label = $"Add {prop.Name.Singularize()} to {type.Name.Singularize()}",
								Method = () =>
								{
									int navId, baseId;
									new InputElement<int>(out navId, $"{prop.Name.Singularize()} ID: ");
									new InputElement<int>(out baseId, $"{type.Name.Singularize()} ID: ");
									var baseEntity = context.Set<TEntity>().Find(baseId);
									var navEntity = navSet.Find(navId);
									dynamic collection = typeof(TEntity).GetProperty(prop.Name).GetValue(baseEntity);
									collection.Add(navEntity);
									context.SaveChanges();
								}
							});

							GUI.SubMenu.Items.Add(new MenuItem
							{
								Label = $"List all {prop.Name.Pluralize()} in {type.Name.Singularize()}",
								Method = () =>
								{
									GUI.ClearOutput();

									var set = context.Set<TEntity>()
										.AsEnumerable() // AHHHHHHHHHHHHHHHHHH
										.Select(p => navProp.GetValue(p));

									GUI.DisplaySet(set);
								}
							});

							GUI.SubMenu.Items.Add(new MenuItem
							{
								Label = $"Remove {prop.Name.Singularize()} from {type.Name.Singularize()}",
								Method = () =>
								{
									int navId, baseId;
									new InputElement<int>(out navId, $"{prop.Name.Singularize()} ID: ");
									new InputElement<int>(out baseId, $"{type.Name.Singularize()} ID: ");
									var baseEntity = context.Set<TEntity>().Find(baseId);
									var navEntity = navSet.Find(navId);
									dynamic collection = typeof(TEntity).GetProperty(prop.Name).GetValue(baseEntity);
									collection.Remove(navEntity);
									context.SaveChanges();
								}
							});

							GUI.SubMenu.Select();
						}
					});
				}
			}
			else
			{
				GUI.NavPropMenu.Visible = false;
			}
		}
	}
}