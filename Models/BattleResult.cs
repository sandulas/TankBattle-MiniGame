namespace TankWars.Models
{

	public class BattleResult
	{
		public string Name;
		public int MatchCount;
		public int Team1Wins;
		public int Team2Wins;

		public Match[] Matches;
	}
}
