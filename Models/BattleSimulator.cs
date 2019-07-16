﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TankWars.Models
{
	public class BattleSimulator
	{
		private readonly TankWarsDbContext _dbContext;
		private BattleRequest battleRequest;

		public BattleSimulator(TankWarsDbContext dbContext, BattleRequest battleRequest)
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

				// Cycle through each team's tanks
				tank1Ix++;
				if (tank1Ix >= battleRequest.Team1TankIds.Length)
					tank1Ix = 0;

				tank2Ix++;
				if (tank2Ix >= battleRequest.Team2TankIds.Length)
					tank2Ix = 0;

				var matchSimulator = new MatchSimulator(battle.Id, tank1, tank2);
				var match = matchSimulator.Simulate();

				_dbContext.Matches.Add(match);
				await _dbContext.SaveChangesAsync();
			}

			return battle;
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
