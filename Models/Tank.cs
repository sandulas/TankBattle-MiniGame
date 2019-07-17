using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TankWars.Models
{
	public class Tank
	{
		public int Id { get; private set; }

		[Required]
		[StringLength(25)]
		public string Name { get; private set; }

		[Required]
		[Range(1, 100)]
		public int Speed { get; private set; }

		[Required]
		[Range(1, 100)]
		public int Accuracy { get; private set; }

		[Required]
		[Range(1, 100)]
		public int MaxAmmo { get; private set; }

		[Required]
		[Range(1, 100)]
		public int Shield { get; private set; }

		[Required]
		[Range(1, 100)]
		public int Range { get; private set; }

		private int health;
		private Vector2 position;
		private int ammo;

		public Tank(string name, int speed, int accuracy, int maxAmmo, int shield, int range)
		{
			this.Name = name;
			this.Speed = speed;
			this.Accuracy = accuracy;
			this.MaxAmmo = maxAmmo;
			this.Shield = shield;
			this.Range = range;
		}

		public void Init()
		{
			this.health = Shield;
			this.ammo = MaxAmmo;
		}

		public void TakeHit()
		{
			if (health > 0) health--;
			else throw new Exception("Tank already destroyed, the match should have ended.");
		}

		public bool Shoot()
		{
			if (ammo == 0) return false;

			if (ammo > 0) ammo--;

			return new Random().Next(100) < Accuracy;
		}

		public int GetHealth() { return health; }

		public Vector2 GetPosition() { return position; }
		public void SetPosition(Vector2 position) { this.position = position; }

		public int GetAmmo() { return ammo; }
	}
}
