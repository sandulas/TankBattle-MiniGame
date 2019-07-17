using System.Threading.Tasks;

namespace TankWars.Models
{
	public class BattleManager
	{
		private readonly TankWarsDbContext _dbContext;
		private BattleRequest battleRequest;

		public BattleManager(TankWarsDbContext dbContext, BattleRequest battleRequest)
		{
			_dbContext = dbContext;
			this.battleRequest = battleRequest;
		}

		public async Task<Battle> SimulateAsync()
		{
			var battle = new Battle(battleRequest.Name);

			_dbContext.Battles.Add(battle);
			await _dbContext.SaveChangesAsync();

			int tank1Ix = 0, tank2Ix = 0;
			for (int i = 0; i < battleRequest.MatchCount; i++)
			{
				var tank1 = await _dbContext.Tanks.FindAsync(battleRequest.Team1TankIds[tank1Ix]);
				var tank2 = await _dbContext.Tanks.FindAsync(battleRequest.Team2TankIds[tank2Ix]);
				tank1.Init();
				tank2.Init();

				// Cycle through each team's tanks
				tank1Ix++;
				if (tank1Ix >= battleRequest.Team1TankIds.Length)
					tank1Ix = 0;

				tank2Ix++;
				if (tank2Ix >= battleRequest.Team2TankIds.Length)
					tank2Ix = 0;
					
				var winner = new MatchSimulator(new Map(), tank1, tank2).Simulate();

				_dbContext.Matches.Add(new Match(battle.Id, tank1.Id, tank2.Id, winner));
				await _dbContext.SaveChangesAsync();
			}

			return battle;
		}
	}
}
