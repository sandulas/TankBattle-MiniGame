using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TankWars.Models
{
	public class Match
	{
		public int Id { get; private set; }

		[Required]
		public int BattleId { get; private set; }

		[Required]
		public int Team1TankId { get; private set; }

		[Required]
		public int Team2TankId { get; private set; }

		[Required]
		[Range(1, 2)]
		public int Winner { get; private set; }

		public Match(int battleId, int team1TankId, int team2TankId, int winner)
		{
			this.BattleId = battleId;
			this.Team1TankId = team1TankId;
			this.Team2TankId = team2TankId;
			this.Winner = winner;
		}
	}
}
