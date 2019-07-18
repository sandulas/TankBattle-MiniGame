using System;
using System.Diagnostics;
using System.Numerics;

namespace TankWars.Models
{
	public class MatchSimulator
	{
		private const int maxSteps = 10000;
		private const float minRange = 20, maxRange = 50;
		private const float minSpeed = 2.5f, maxSpeed = 5;
		private const float obstacleDetectionDistance = 15;
		private const int reloadSteps = 100;
		private const float lowAmmoRatio = .25f;

		private Map map;
		private TankUnit tankUnit1;
		private TankUnit tankUnit2;

		public MatchSimulator(Map map, Tank tank1, Tank tank2)
		{
			this.map = map;
			tankUnit1 = new TankUnit(this, tank1);
			tankUnit2 = new TankUnit(this, tank2);
		}

		public int Simulate()
		{
			tankUnit1.Tank.SetPosition(map.StartPosition1);
			tankUnit2.Tank.SetPosition(map.StartPosition2);

			int step = 0;
			int winner;

			while (true)
			{
				simulateTurn(tankUnit1, tankUnit2, step);
				simulateTurn(tankUnit2, tankUnit1, step);

				// match is over, at least one tank is destroyed
				if (tankUnit1.Tank.GetHealth() == 0 || tankUnit2.Tank.GetHealth() == 0)
				{
					if (tankUnit1.Tank.GetHealth() == 0 && tankUnit2.Tank.GetHealth() == 0) winner = 0;
					else if (tankUnit1.Tank.GetHealth() == 0) winner = 2;
					else winner = 1;

					return winner;
				}

				// fail-safe: a draw is returned if the match lasts more than maxSteps
				step++;
				if (step > maxSteps) return 0;
			}
		}

		private void simulateTurn(TankUnit activeUnit, TankUnit enemyUnit, int step)
		{
			if (step % reloadSteps == 0 && activeUnit.HasLowAmmo()) activeUnit.Tank.Reload();

			// enemy in range and in line of sight
			if (activeUnit.IsEnemyInRange(enemyUnit) && activeUnit.HasClearShot(enemyUnit))
			{
				if (activeUnit.Tank.Shoot()) enemyUnit.Tank.TakeHit();

				// run away if low on ammo and in enemy range
				if (activeUnit.HasLowAmmo() && activeUnit.IsInEnemyRange(enemyUnit))
				{
					activeUnit.MoveTowards(activeUnit.Tank.GetPosition() - (enemyUnit.Tank.GetPosition() - activeUnit.Tank.GetPosition()));
				}
			}
			// enemy not in range or not in line of sight
			else
			{
				// advance towards the enemy and shoot if in range
				activeUnit.MoveTowards(enemyUnit.Tank.GetPosition());

				if (activeUnit.IsEnemyInRange(enemyUnit) && activeUnit.HasClearShot(enemyUnit))
				{
					if (activeUnit.Tank.Shoot()) enemyUnit.Tank.TakeHit();
				}
			}
		}

		public class TankUnit
		{
			public MatchSimulator Match { get; }
			public Tank Tank { get; }
			public float Range { get; }
			public float Speed { get; }

			public TankUnit(MatchSimulator match, Tank tank)
			{
				Match = match;
				Tank = tank;
				Range = minRange + (maxRange - minRange) * tank.Range / 100;
				Speed = minSpeed + (maxSpeed - minSpeed) * tank.Speed / 100;
			}

			public bool IsEnemyInRange(TankUnit enemyUnit)
			{
				return Range > Vector2.Distance(Tank.GetPosition(), enemyUnit.Tank.GetPosition());
			}

			public bool IsInEnemyRange(TankUnit enemyUnit)
			{
				return enemyUnit.Range > Vector2.Distance(Tank.GetPosition(), enemyUnit.Tank.GetPosition());
			}

			public bool HasClearShot(TankUnit enemyUnit)
			{
				if (Match.map.GetObstacleDistance(Tank.GetPosition(), enemyUnit.Tank.GetPosition()) == null) return true;
				return false;
			}

			public bool HasLowAmmo()
			{
				if (Tank.GetAmmo() < Tank.MaxAmmo * lowAmmoRatio) return true;
				return false;
			}

			public void MoveTowards(Vector2 point)
			{
				// detect and avoid obstacles
				Vector2 detectionOffset = Vector2.Normalize(point - Tank.GetPosition()) * obstacleDetectionDistance;

				var obstacleDistance = Match.map.GetObstacleDistance(Tank.GetPosition(), Tank.GetPosition() + detectionOffset);

				// obstacle detected
				if (obstacleDistance != null)
				{
					// try change direction
					float rotationDegrees = 1;
					var newDetectionOffset = Tools.RotateVector2(detectionOffset, rotationDegrees);
					var newObstacleDistance = Match.map.GetObstacleDistance(Tank.GetPosition(), Tank.GetPosition() + newDetectionOffset);

					// obstacle no longer detected
					if (newObstacleDistance == null)
					{
						Tank.SetPosition(Tank.GetPosition() + Vector2.Normalize(newDetectionOffset) * Speed);
						return;
					}
					// obstacle even closer, reverse direction
					else if (newObstacleDistance < obstacleDistance)
					{
						rotationDegrees = -rotationDegrees;
					}

					newDetectionOffset = detectionOffset;
					float totalRotation = 0;

					// change direction until no obstacle is detected
					while (totalRotation < 360)
					{
						newDetectionOffset = Tools.RotateVector2(newDetectionOffset, rotationDegrees);
						newObstacleDistance = Match.map.GetObstacleDistance(Tank.GetPosition(), Tank.GetPosition() + newDetectionOffset);

						if (newObstacleDistance == null)
						{
							Tank.SetPosition(Tank.GetPosition() + Vector2.Normalize(newDetectionOffset) * Speed);
							return;
						}

						totalRotation += rotationDegrees;
					}
				}
				// obstacle not detected
				else
				{
					Tank.SetPosition(Tank.GetPosition() + Vector2.Normalize(detectionOffset) * Speed);
				}
			}
		}

	}
}
