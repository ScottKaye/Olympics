using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEditor.Editors
{
	public abstract class Editor
	{
		private IEnumerable<EntityType> Types { get; set; }

		public Editor(IEnumerable<EntityType> types)
		{
			Types = types;
		}

		/// <summary>
		/// Initialize the editor
		/// </summary>
		public abstract void Start();


	}
}
