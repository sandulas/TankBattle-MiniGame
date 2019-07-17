using System.Threading.Tasks;

namespace TankWars.Models
{
	public class MatchSimulator
	{
		private const int maxSteps = 10000;

		private int battleId;
		private Tank tank1;
		private Tank tank2;

		public MatchSimulator(int battleId, Tank tank1, Tank tank2)
		{
			this.battleId = battleId;
			this.tank1 = tank1;
			this.tank2 = tank2;
		}

		public Match Simulate()
		{
			int winner = doSimulation();
			return new Match(battleId, tank1.Id, tank2.Id, winner);
		}

		private int doSimulation()
		{
			int steps = 0;
			int winner;

			while (true)
			{
				if (tank1.Shoot()) tank2.TakeHit();
				if (tank2.Shoot()) tank1.TakeHit();

				// match is over, at least one tank is destroyed
				if (tank1.GetHealth() == 0 || tank2.GetHealth() == 0)
				{
					if (tank1.GetHealth() == 0 && tank2.GetHealth() == 0) winner = 0;
					else if (tank1.GetHealth() == 0) winner = 2;
					else winner = 1;

					return winner;
				}

				// fail-safe: a draw is returned if the match lasts more than maxSteps
				steps++;
				if (steps > maxSteps) return 0;
			}
		}
	}
}
