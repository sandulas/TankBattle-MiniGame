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
			return $@"Welcome to the Battle API. Operations:
- POST /battle/simulate
- GET /battle/list
- GET /battle/id";
		}

		// POST api/battle/simulate
		[HttpPost("simulate")]
		public async Task<ActionResult<Battle>> SimulateBattle(Battle battle)
		{
			_dbContext.Battles.Add(battle);
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBattle), new { id = battle.Id }, battle);
		}

		// GET api/tank/list
		//[HttpGet("list")]
		//public async Task<ActionResult<IEnumerable<Tank>>> GetTankList()
		//{
		//	return await _dbContext.Tanks.ToListAsync();
		//}

		// GET api/tank/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Battle>> GetBattle(int id)
		{
			var battle = await _dbContext.Battles.FindAsync(id);

			if (battle == null)
				return NotFound();

			return battle;
		}
	}
}
