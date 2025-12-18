using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RadioScheduler.Models;
using RadioScheduler.Models.Auth;
using RadioScheduler.Utils.Enum;

namespace RadioScheduler.Utils;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
	public DbSet<User> UserDb { get; set; }
	public DbSet<Role> RoleDb { get; set; }

	public DbSet<UserRole> UserRoleDb { get; set; }
	public DbSet<UserLogin> UserLoginDb { get; set; }

	public DbSet<Accounting> AccountingDb { get; set; }
	public DbSet<AccountingRow> AccountingRowDb { get; set; }
	public DbSet<ContributorPayment> ContributorPaymentDb { get; set; }

	public DbSet<RadioShow> RadioShow { get; set; }
	public DbSet<RadioHost> RadioHost { get; set; }
	public DbSet<Studio> Studio { get; set; }
	public DbSet<Tableau> Tableau { get; set; }
	public DbSet<Timeslot> Timeslot { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder
			.UseSqlite("Data Source=localDB.db")
			.EnableSensitiveDataLogging()
			.LogTo(Console.WriteLine, LogLevel.Information);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Role>(ConfigureRole);
		modelBuilder.Entity<User>(ConfigureUser);
		modelBuilder.Entity<UserRole>(ConfigureUserRole);
		modelBuilder.Entity<UserLogin>(ConfigureUserLogin);
		modelBuilder.Entity<ContributorPayment>(ConfigureContributorPayment);

		modelBuilder.Entity<Accounting>(ConfigureAccounting);
		modelBuilder.Entity<AccountingRow>(ConfigureAccountingRow);

		modelBuilder.Entity<RadioShow>().ToTable("global_radio_show");
		modelBuilder.Entity<RadioHost>().ToTable("global_radio_host");
		modelBuilder.Entity<Studio>().ToTable("global_studio");

		modelBuilder.Entity<Tableau>(ConfigureTableau);
		modelBuilder.Entity<Timeslot>(ConfigureTimeslot);

		foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()) {
			entityType.SetTableName(entityType.GetTableName()?.ToLower());
			foreach (IMutableProperty property in entityType.GetProperties()) {
				property.SetColumnName(ToSnakeCase(property.GetColumnName()));
			}
		}
	}

	private static void ConfigureUser(EntityTypeBuilder<User> builder) {
		builder.ToTable("global_user");

		builder.HasKey(u => u.Id);

		builder.Property(u => u.Id)
			.HasColumnName("id")
			.HasConversion(v => v.ToString(), v => Guid.Parse(v))
			.IsRequired();

		builder.Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(255);
		builder.Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(255);
		builder.Property(u => u.Username).HasColumnName("username").HasMaxLength(255).IsRequired();
		builder.Property(u => u.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
		builder.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(50);
		builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(255);
		builder.Property(u => u.Address).HasColumnName("address").HasMaxLength(255);
		builder.Property(u => u.City).HasColumnName("city").HasMaxLength(255);
		builder.Property(u => u.ZipCode).HasColumnName("zip_code").HasMaxLength(50);

		builder.Property(u => u.CreatedAt)
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.IsRequired();

		builder.HasMany(u => u.Roles)
			.WithOne(ur => ur.User)
			.HasForeignKey(ur => ur.UserId);

		builder
			.HasMany<UserLogin>()
			.WithOne(ul => ul.User)
			.HasForeignKey(ul => ul.UserId)
			.IsRequired();
	}

	private static void ConfigureRole(EntityTypeBuilder<Role> builder) {
		builder.ToTable("global_role");

		builder.HasKey(r => r.Id);

		builder.Property(r => r.Id)
			.HasColumnName("id")
			.HasConversion(v => v.ToString(), v => Guid.Parse(v))
			.IsRequired();

		builder.Property(r => r.Code)
			.HasConversion<string>()
			.HasColumnName("role_code")
			.IsRequired();

		builder.Property(r => r.Title)
			.HasColumnName("role_title")
			.HasMaxLength(255);
	}

	private static void ConfigureUserRole(EntityTypeBuilder<UserRole> builder) {
		builder.ToTable("user_role");

		builder.HasKey(ur => new { ur.UserId, ur.RoleId });

		builder.HasOne(ur => ur.User)
			.WithMany(ur => ur.Roles)
			.HasForeignKey(ur => ur.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(ur => ur.Role)
			.WithMany()
			.HasForeignKey(ur => ur.RoleId)
			.OnDelete(DeleteBehavior.Cascade);
	}

	private static void ConfigureUserLogin(EntityTypeBuilder<UserLogin> builder) {
		builder.ToTable("user_login");

		builder.HasKey(l => l.Id);

		builder.Property(l => l.Id)
			.HasColumnName("id")
			.HasConversion(v => v.ToString(), v => Guid.Parse(v))
			.IsRequired();

		builder.Property(l => l.LoginTime)
			.HasColumnName("login_time")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.IsRequired();

		builder.Property(l => l.UserId)
			.HasColumnName("user_id")
			.IsRequired();
	}

	private static void ConfigureAccounting(EntityTypeBuilder<Accounting> builder) {
		builder.ToTable("accounting");

		builder.HasKey(a => a.Id);

		builder
			.HasMany(a => a.AccountingRows)
			.WithOne(a => a.Accounting)
			.HasForeignKey(a => a.AccountingId)
			.IsRequired();
	}

	private static void ConfigureAccountingRow(EntityTypeBuilder<AccountingRow> builder) {
		builder.ToTable("accounting_row");

		builder.HasKey(r => r.Id);

		builder.Property(u => u.AccountNumber).HasColumnName("account_number").HasMaxLength(255);
		builder.Property(u => u.AccountName).HasColumnName("account_name").HasMaxLength(255);
	}

	private static void ConfigureContributorPayment(EntityTypeBuilder<ContributorPayment> builder) {
		builder.ToTable("contributor_payment");

		builder.HasKey(l => l.Id);

		builder
			.HasOne(c => c.User)
			.WithMany(u => u.ContributorPayments)
			.HasForeignKey(l => l.UserId)
			.IsRequired();

		builder
			.HasOne(c => c.Accounting)
			.WithOne(a => a.ContributorPayment)
			.HasForeignKey<ContributorPayment>(l => l.AccountingId)
			.IsRequired();

		builder.Property(c => c.PaymentDate)
			.HasColumnName("payment_date")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.IsRequired();
	}

	private static void ConfigureTableau(EntityTypeBuilder<Tableau> builder) {
		builder.ToTable("global_tableau");

		builder.HasKey(t => t.Id);

		builder.HasMany(t => t.Timeslots)
			.WithOne(t => t.Tableau)
			.HasForeignKey(t => t.TableauId)
			.OnDelete(DeleteBehavior.Cascade);
	}

	private static void ConfigureTimeslot(EntityTypeBuilder<Timeslot> builder) {
		builder.ToTable("timeslot");

		builder.HasKey(t => t.Id);

		builder.HasOne(ts => ts.RadioShow)
			.WithMany()
			.HasForeignKey(ts => ts.RadioShowId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasOne(ts => ts.Studio)
			.WithMany()
			.HasForeignKey(ts => ts.StudioId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasMany(ts => ts.RadioHosts)
			.WithMany(rh => rh.Timeslots)
			.UsingEntity(j => {
				j.ToTable("timeslot_host");
				j.HasKey("TimeslotsId", "RadioHostsId");
				j.HasIndex("TimeslotsId", "RadioHostsId");
			});

		builder.HasIndex(ts => new { ts.StudioId, ts.StartTime, ts.EndTime });

		builder.HasIndex(ts => ts.StudioId)
			.IsUnique();

		builder.HasIndex(ts => ts.RadioShowId)
			.IsUnique();

		builder.Ignore(ts => ts.RadioHostIds);
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
