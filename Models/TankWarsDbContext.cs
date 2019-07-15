using Microsoft.EntityFrameworkCore;

namespace TankWars.Models
{
	public class TankWarsDbContext : DbContext
	{
		public TankWarsDbContext(DbContextOptions<TankWarsDbContext> options) : base(options)
		{
		}

		public DbSet<Tank> Tanks { get; set; }
		public DbSet<Battle> Battles { get; set; }
		public DbSet<Match> Matches { get; set; }
	}
}
