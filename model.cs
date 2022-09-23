using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace aem4
{
	public partial class model : DbContext
	{
		public model()
			: base("name=modelConn")
		{
		}

		public virtual DbSet<platform> platform { get; set; }
		public virtual DbSet<well> well { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<platform>()
				.Property(e => e.latitude)
				.HasPrecision(18, 6);

			modelBuilder.Entity<platform>()
				.Property(e => e.longitude)
				.HasPrecision(18, 6);

			modelBuilder.Entity<well>()
				.Property(e => e.latitude)
				.HasPrecision(18, 6);

			modelBuilder.Entity<well>()
				.Property(e => e.longitude)
				.HasPrecision(18, 6);
		}
	}
}
