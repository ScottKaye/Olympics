using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Olympics.Models
{
	public class Athlete
	{
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		public int Age { get; set; }

		[Required]
		public string Gender { get; set; }

		public int Country_Id { get; set; }

		public virtual Country Country { get; set; }

		public virtual ICollection<Sport> Sports { get; set; } = new HashSet<Sport>();
	}
}