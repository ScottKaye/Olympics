using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Olympics.Models
{
	public class Country
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int Population { get; set; }

		public virtual ICollection<Athlete> Athletes { get; set; } = new HashSet<Athlete>();
	}
}