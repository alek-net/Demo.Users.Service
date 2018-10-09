using Microsoft.EntityFrameworkCore;

namespace UsersRegistration
{
	public class UsersDbContext : DbContext
	{
		private string _connString;

		public UsersDbContext(string connString)
		{
			_connString = connString;
		}

		public UsersDbContext(DbContextOptions<UsersDbContext> options)
		: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
			.HasIndex(user => new { user.TenantId, user.Email })
			.IsUnique();

			modelBuilder.Entity<Tenant>()
				.HasData(
				new Tenant { TenantId = 1, Domain = "mrgreen.com" },
				new Tenant { TenantId = 2, Domain = "redbet.com" });

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connString);
			}
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Tenant> Tenants { get; set; }
	}
}
