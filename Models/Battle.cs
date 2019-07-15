using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TankWars.Models
{
	public class Battle
	{
		public int Id { get; private set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[NotMapped]
		public int[] Team1TankIds { get; set; }

		[Required]
		[NotMapped]
		public int[] Team2TankIds { get; set; }

		//public Battle(string name)
		//{
		//	this.Name = name;
		//	this.Team1TankIds = team1TankIds;
		//	this.Team2TankIds = team2TankIds;
		//}
	}
}
