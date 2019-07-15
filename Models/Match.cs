using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TankWars.Models
{
	public class Match
	{
		public int Id { get; private set; }

		[Required]
		public Battle Battle { get; private set; }

		[Required]
		public Tank Team1Tank { get; private set; }

		[Required]
		public Tank Team2Tank { get; private set; }

		[Required]
		[Range(1, 2)]
		public int Winner { get; private set; }

		public Match() { }

		public Match(Battle battle, Tank team1Tank, Tank team2Tank, int winner)
		{
			this.Battle = battle;
			this.Team1Tank = team1Tank;
			this.Team2Tank = team2Tank;
			this.Winner = winner;
		}
	}
}
