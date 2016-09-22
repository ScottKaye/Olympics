using System.Data.Entity;

namespace Olympics.Models
{
	public class OlympicsContext : DbContext
	{
		public OlympicsContext()
			: base("name=OlympicsContext") { }

		public virtual DbSet<Athlete> Athletes { get; set; }
		public virtual DbSet<Country> Countries { get; set; }
		public virtual DbSet<Medium> Media { get; set; }
		public virtual DbSet<Sport> Sports { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Athlete>()
				.HasMany(e => e.Sports)
				.WithMany(e => e.Athletes)
				.Map(m => m.ToTable("AthleteSport").MapLeftKey("Athletes_Id").MapRightKey("Sports_Id"));

			modelBuilder.Entity<Country>()
				.HasMany(e => e.Athletes)
				.WithRequired(e => e.Country)
				.HasForeignKey(e => e.Country_Id)
				.WillCascadeOnDelete(false);
		}
	}
}