using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RadioScheduler.Models;

namespace RadioScheduler.Utils;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
	public DbSet<RadioShow> RadioShow { get; set; }
	public DbSet<RadioHost> RadioHost { get; set; }
	public DbSet<Studio> Studio { get; set; }
	public DbSet<Timeslot> Timeslot { get; set; }
	public DbSet<Tableau> Tableau { get; set; }
	public DbSet<Schedule> Schedule { get; set; }

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

		foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()) {
			entityType.SetTableName(entityType.GetTableName()?.ToLower());
			foreach (IMutableProperty property in entityType.GetProperties()) {
				property.SetColumnName(ToSnakeCase(property.GetColumnName()));
			}
		}
	}

	private static string ToSnakeCase(string? input) {
		if (string.IsNullOrEmpty(input)) {
			return input ?? string.Empty;
		}

		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < input.Length; i++) {
			char c = input[i];
			if (char.IsUpper(c) && i > 0) {
				stringBuilder.Append('_');
			}

			stringBuilder.Append(char.ToLower(c));
		}
		return stringBuilder.ToString();
	}
}
