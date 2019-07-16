using System.ComponentModel.DataAnnotations;

namespace TankWars.Models
{
	public class BattleRequest
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		public int[] Team1TankIds { get; set; }

		[Required]
		public int[] Team2TankIds { get; set; }

		[Required]
		[Range(1, 100)]
		public int MatchCount { get; set; }
	}
}
