using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TankWars.Models;

namespace TankWars.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BattleController : ControllerBase
	{
		private readonly TankWarsDbContext _dbContext;

		public BattleController(TankWarsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET api/battle
		[HttpGet]
		public ActionResult<string> Welcome()
		{
			return $@"Welcome to the Battle API.";
		}

		// POST api/battle/simulate
		[HttpPost("simulate")]
		public async Task<ActionResult<BattleRequest>> SimulateBattle(BattleRequest battleRequest)
		{
			if (battleRequest.Team1TankIds.Length == 0 || battleRequest.Team2TankIds.Length == 0)
				return BadRequest();

			foreach (int tankId in battleRequest.Team1TankIds)
			{
				if (await _dbContext.Tanks.FindAsync(tankId) == null) return NotFound(new { tankId });
			}

			foreach (int tankId in battleRequest.Team2TankIds)
			{
				if (await _dbContext.Tanks.FindAsync(tankId) == null) return NotFound(new { tankId });
			}

			BattleManager battleManager = new BattleManager(_dbContext, battleRequest);

			Battle battle = await battleManager.SimulateAsync();

			return CreatedAtAction(nameof(GetBattleResult), new { id = battle.Id }, battle);
		}

		// GET api/battle/list
		[HttpGet("list")]
		public async Task<ActionResult<IEnumerable<Battle>>> GetBattleList()
		{
			return await _dbContext.Battles.ToListAsync();
		}

		// GET api/battle/result/{id}
		[HttpGet("result/{id}")]
		public async Task<ActionResult<BattleResult>> GetBattleResult(int id)
		{
			var battle = await _dbContext.Battles.FindAsync(id);

			if (battle == null)
				return NotFound();

			var matches = await _dbContext.Matches.Where(x => x.BattleId == id).ToListAsync();

			BattleResult battleResult = new BattleResult()
			{
				Name = battle.Name,
				MatchCount = matches.Count(),
				Team1Wins = matches.Count(x => x.Winner == 1),
				Team2Wins = matches.Count(x => x.Winner == 2),
				Matches = matches.ToArray()
			};

			return battleResult;
		}
	}
}
