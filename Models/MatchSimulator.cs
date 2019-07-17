using System;
using System.Numerics;

namespace TankWars.Models
{
	public class MatchSimulator
	{
		private const int maxSteps = 10000;
		private const float minRange = 20, maxRange = 100;

		private Map map;
		private Tank tank1;
		private Tank tank2;

		public MatchSimulator(Map map, Tank tank1, Tank tank2)
		{
			this.map = map;
			this.tank1 = tank1;
			this.tank2 = tank2;
		}

		public int Simulate()
		{
			tank1.SetPosition(map.StartPosition1);
			tank2.SetPosition(map.StartPosition2);

			int steps = 0;
			int winner;

			while (true)
			{
				doSimulationStep();

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

		private void doSimulationStep()
		{
			if (tank1.Shoot()) tank2.TakeHit();
			if (tank2.Shoot()) tank1.TakeHit();
		}
	}
}
