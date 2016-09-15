using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEditor.Editors
{
	public class CLI : Editor
	{
		public CLI(IEnumerable<EntityType> types) : base(types) { }

		public override void Start()
		{
			throw new NotImplementedException();
		}
	}
}
