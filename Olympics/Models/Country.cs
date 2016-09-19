namespace Olympics.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class Country
	{
		public Country()
		{
			Athletes = new HashSet<Athlete>();
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int Population { get; set; }

		public virtual ICollection<Athlete> Athletes { get; set; }
	}
}