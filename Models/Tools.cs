using System;
using System.Numerics;

namespace TankWars.Models
{
	// various math and calculus methods
	public static class Tools
	{
		public static float PointToSegmentDistance(Vector2 p, Vector2 s1, Vector2 s2)
		{
			// project the point on the segment
			var t = -((s1.X - p.X) * (s2.X - s1.X) + (s1.Y - p.Y) * (s2.Y - s1.Y)) / (Sqr(s2.X - s1.X) + Sqr(s2.Y - s1.Y));

			// if the projection is outside the segment return the shorter distance between the point and the ends of the segment
			if (t < 0 || t > 1)
			{
				var d1 = Vector2.Distance(p, s1);
				var d2 = Vector2.Distance(p, s2);
				return Math.Min(d1, d2);
			}
			// if the projection is inside the segment return the distance to the projection
			else
			{
				return Math.Abs((s2.X - s1.X) * (s1.Y - p.Y) - (s2.Y - s1.Y) * (s1.X - p.X)) / Vector2.Distance(s1, s2);
			}
		}

		public static Vector2 RotateVector2(Vector2 vector, float degrees)
		{
			Vector2 result;
			double angleRadians = Radians(degrees);

			result.X = (float)(vector.X * Math.Cos(angleRadians) - vector.Y * Math.Sin(angleRadians));
			result.Y = (float)(vector.X * Math.Sin(angleRadians) + vector.Y * Math.Cos(angleRadians));

			return result;
		}

		public static float Sqr(float number)
		{
			return number * number;
		}

		public static double Radians(float angleInDegrees)
		{
			return Math.PI / 180 * angleInDegrees;
		}
	}
}
