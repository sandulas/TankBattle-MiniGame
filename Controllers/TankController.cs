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
	public class TankController : ControllerBase
	{
		private readonly TankWarsDbContext _dbContext;

		public TankController(TankWarsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET api/tank
		[HttpGet]
		public ActionResult<string> Welcome()
		{
			return $@"Welcome to the Tank API. Operations:
- POST /tank
- GET /tank/list
- GET /tank/id
- DELETE /tank/id";
		}

		// POST api/tank
		[HttpPost]
		public async Task<ActionResult<Tank>> AddTank(Tank tank)
		{
			_dbContext.Tanks.Add(tank);
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTank), new { id = tank.Id }, tank);
		}

		// GET api/tank/list
		[HttpGet("list")]
		public async Task<ActionResult<IEnumerable<Tank>>> GetTankList()
		{
			return await _dbContext.Tanks.ToListAsync();
		}

		// GET api/tank/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Tank>> GetTank(int id)
		{
			var tank = await _dbContext.Tanks.FindAsync(id);

			if (tank == null)
				return NotFound();

			return tank;
		}

		// DELETE api/tank/{id}
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTank(int id)
		{
			var todoItem = await _dbContext.Tanks.FindAsync(id);

			if (todoItem == null)
				return NotFound();

			_dbContext.Tanks.Remove(todoItem);
			await _dbContext.SaveChangesAsync();

			return NoContent();
		}
	}
}
