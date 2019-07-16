using System.ComponentModel.DataAnnotations;

namespace TankWars.Models
{
	public class Battle
	{
		public int Id { get; private set; }

		[Required]
		[StringLength(100)]
		public string Name { get; private set; }

		public Battle(string name)
		{
			this.Name = name;
		}
	}
}
