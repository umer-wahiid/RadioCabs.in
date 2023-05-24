using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
	public class RCDbContext : DbContext
	{
		public RCDbContext(DbContextOptions<RCDbContext> options) : base(options) { }

		public DbSet<CompanyRegistration> CompanyRegistrations { get; set; }
		public DbSet<DriversRegistration> DriversRegistrations { get; set; }
		public DbSet<AdvertiseRegistration> AdvertiseRegistrations { get; set; }
		public DbSet<FeedBack> FeedBacks { get; set; }
		public DbSet<Registration> Registration { get; set; }
	}
}
