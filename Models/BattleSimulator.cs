using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TankWars.Models
{
	public class BattleSimulator
	{
		private BattleRequest battleRequest;

		public BattleSimulator(BattleRequest battleRequest)
		{
			this.battleRequest = battleRequest;
		}

		public void Simulate()
		{

		}
	}

	public class MatchSimulator
	{
		private int battleId;
		private Tank tank1;
		private Tank tank2;

		private int steps;

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
			steps = 0;

			do
			{
				steps++;
				if (steps > 100) return 1;
			}
			while (step());

			return 1;
		}

		private bool step()
		{
			return true;
		}
	}
}
