namespace Olympics.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class Athlete
    {
        public Athlete()
        {
            Sports = new HashSet<Sport>();
        }

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

        public virtual ICollection<Sport> Sports { get; set; }
    }
}