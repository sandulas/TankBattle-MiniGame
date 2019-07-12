using Microsoft.EntityFrameworkCore;

namespace TankWars.Models
{
	public class TankWarsDbContext : DbContext
	{
		public TankWarsDbContext(DbContextOptions<TankWarsDbContext> options) : base(options)
		{
		}

		public DbSet<Tank> Tanks { get; set; }
	}
}
