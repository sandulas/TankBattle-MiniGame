using System.ComponentModel.DataAnnotations;

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

		public Tank(string name, int speed, int accuracy, int maxAmmo, int shield, int range)
		{
			this.Name = name;
			this.Speed = speed;
			this.Accuracy = accuracy;
			this.MaxAmmo = maxAmmo;
			this.Shield = shield;
			this.Range = range;

			this.health = shield;
		}

		public void TakeHit()
		{
			if (health > 0) health--;
		}
	}
}
