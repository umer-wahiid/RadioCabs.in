using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
	public class RCDbContext : DbContext
	{
		public RCDbContext(DbContextOptions<RCDbContext> options) : base(options) { }
	}
}
