using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Olympics.Models
{
	public class Sport
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Rules { get; set; }

		public virtual ICollection<Athlete> Athletes { get; set; } = new HashSet<Athlete>();
	}
}