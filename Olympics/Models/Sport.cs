namespace Olympics.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class Sport
	{
		public Sport()
		{
			Athletes = new HashSet<Athlete>();
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Rules { get; set; }

		public virtual ICollection<Athlete> Athletes { get; set; }
	}
}