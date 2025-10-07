using Microsoft.EntityFrameworkCore;
using RadioScheduler.Models;

namespace RadioScheduler.Utils;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
	public DbSet<RadioShow> RadioShows { get; set; }
	public DbSet<RadioHost> RadioHosts { get; set; }
	public DbSet<Studio> Studios { get; set; }
	public DbSet<Timeslot> Timeslot { get; set; }
	public DbSet<Tableau> Tableaux { get; set; }
	public DbSet<Schedule> Schedules { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<RadioShow>().ToTable("global_radio_show");
		modelBuilder.Entity<RadioHost>().ToTable("global_radio_host");
		modelBuilder.Entity<Studio>().ToTable("global_studio");
		modelBuilder.Entity<Schedule>().ToTable("global_schedule");

		modelBuilder.Entity<Timeslot>()
			.HasMany(t => t.RadioHosts)
			.WithMany()
			.UsingEntity<Dictionary<string, object>>(
				"timeslot_host",
				j => j.HasOne<RadioHost>().WithMany().HasForeignKey("host_id"),
				j => j.HasOne<Timeslot>().WithMany().HasForeignKey("timeslot_id")
			);
	}
}
