using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Controllers
{
	class GenericController<TEntity, TContext> : IController<TEntity>
		where TEntity : class, new()
		where TContext : DbContext, new()
	{
		public virtual void Create(params Expression<Func<TEntity, object>>[] editableProps)
		{
			using (var db = new TContext())
			{
				var entity = new TEntity();

				// Get editable props
				foreach (var propName in editableProps)
				{
					var member = propName.Member();
					var propInfo = typeof(TEntity).GetProperty(member.Name);

					Console.Write($"{member.Name}: ");
					string newValue = Console.ReadLine().Trim();
					propInfo.SetValue(entity, newValue);
				}

				Console.WriteLine("Saving.");
				db.Set<TEntity>().Add(entity);
				db.SaveChanges();
			}
		}

		public virtual void Read(params Expression<Func<TEntity, object>>[] viewableProps)
		{
			Console.WriteLine(string.Join(" | ", viewableProps.Select(p => p.Member().Name)));

			using (var db = new TContext())
			{
				foreach (var entity in db.Set<TEntity>())
				{
					List<string> columns = new List<string>();

					foreach (var propName in viewableProps)
					{
						var member = propName.Member();
						var propInfo = typeof(TEntity).GetProperty(member.Name);
						var currentValue = propInfo.GetValue(entity);
						columns.Add(currentValue.ToString());
					}

					Console.WriteLine(string.Join(" | ", columns));
				}
			}
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

			using (var db = new TContext())
			{
				var entity = db.Set<TEntity>().Find(id);

				// Get editable props
				foreach (var propName in editableProps)
				{
					var member = propName.Member();
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
			}
		}

		public virtual void Delete()
		{
			int id;
			if (!PromptForId(out id))
			{
				Console.WriteLine("No ID specified.");
				return;
			}

			using (var db = new TContext())
			{
				var entity = db.Set<TEntity>().Find(id);
				db.Set<TEntity>().Remove(entity);
				db.SaveChanges();
			}
		}

		private bool PromptForId(out int id)
		{
			Console.Write($"Enter the ID of the {typeof(TEntity).Name}: ");
			return int.TryParse(Console.ReadLine(), out id);
		}
	}
}
