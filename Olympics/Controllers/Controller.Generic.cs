using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Olympics.Controllers
{
	class GenericController<TEntity, TContext> : IController<TEntity>
		where TEntity : class, new()
		where TContext : DbContext, new()
	{
		public virtual void Create(params Expression<Func<TEntity, object>>[] editableProps)
		{
			var entity = new TEntity();

			Contextual(db =>
			{
				// Get editable props
				foreach (var propName in editableProps)
				{
					var member = propName.GetMember();
					var propInfo = typeof(TEntity).GetProperty(member.Name);

					Console.Write($"{member.Name}: ");
					string newValue = Console.ReadLine().Trim();
					var casted = Convert.ChangeType(newValue, propInfo.PropertyType);
					propInfo.SetValue(entity, casted);
				}

				Console.WriteLine("Saving.");
				db.Set<TEntity>().Add(entity);
				db.SaveChanges();
			});
		}2

		public virtual void Read(params Expression<Func<TEntity, object>>[] viewableProps)
		{
			Console.WriteLine(string.Join(" | ", viewableProps.Select(p => p.GetMember().Name)));

			Contextual(db =>
			{
				foreach (var entity in db.Set<TEntity>())
				{
					List<string> columns = new List<string>();

					foreach (var propName in viewableProps)
					{
						var member = propName.GetMember();
						var propInfo = typeof(TEntity).GetProperty(member.Name);
						var currentValue = propInfo.GetValue(entity);
						columns.Add(currentValue.ToString());
					}

					Console.WriteLine(string.Join(" | ", columns));
				}
			});
		}

		public virtual void Update(params Expression<Func<TEntity, object>>[] editableProps)
		{
			int id;
			if (!PromptForId(out id))
			{
				Console.WriteLine("No ID specified.");
				return;
			}

			Console.WriteLine("Enter nothing to keep the current value.");

			Contextual(db =>
			{
				var entity = db.Set<TEntity>().Find(id);

				// Get editable props
				foreach (var propName in editableProps)
				{
					var member = propName.GetMember();
					var propInfo = typeof(TEntity).GetProperty(member.Name);

					var currentValue = propInfo.GetValue(entity);

					Console.Write($"New value for {member.Name} (currently {currentValue}): ");
					string newValue = Console.ReadLine().Trim();

					if (newValue.Length > 0)
					{
						var casted = Convert.ChangeType(newValue, propInfo.PropertyType);
						propInfo.SetValue(entity, casted);
					}
				}

				Console.WriteLine("Updating.");
				db.SaveChanges();
			});
		}

		public virtual void Delete()
		{
			int id;
			if (!PromptForId(out id))
			{
				Console.WriteLine("No ID specified.");
				return;
			}

			Contextual(db =>
			{
				var entity = db.Set<TEntity>().Find(id);
				db.Set<TEntity>().Remove(entity);
				db.SaveChanges();
			});
		}

		private T Contextual<T>(Func<TContext, T> callback)
		{
			using (var db = new TContext())
			{
				return callback(db);
			}
		}

		private void Contextual(Action<TContext> callback)
		{
			using (var db = new TContext())
			{
				callback(db);
			}
		}

		private bool PromptForId(out int id)
		{
			Console.Write($"Enter the ID of the {typeof(TEntity).Name}: ");
			return int.TryParse(Console.ReadLine(), out id);
		}
	}
}
