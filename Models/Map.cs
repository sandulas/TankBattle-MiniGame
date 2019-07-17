using System;
using System.Collections.Generic;
using System.Numerics;

namespace TankWars.Models
{
	public class Map
	{
		private const int mapSize = 140; // the map is a square, the size is the side of the square
		private const int cellSize = 20; //the map is divided into a grid of square cells
		private const float tankRadius = 2.5f; // the tank is represented as a circle
		private const int obstacleProbability = 50; // likelihood of an ostacle being created

		public int Size { get; }
		public List<Obstacle> Obstacles { get; }
		public Vector2 StartPosition1 { get; }
		public Vector2 StartPosition2 { get; }

		// the obstacles are circles of various sizes
		public struct Obstacle
		{
			public Vector2 Center { get; }
			public float Radius { get; }

			public Obstacle(Vector2 center, float radius)
			{
				Center = center;
				Radius = radius;
			}
		}

		public Map()
		{
			Size = mapSize;
			Obstacles = new List<Obstacle>();

			var cellCount = mapSize / cellSize; // number of cells per line / column
			var evenCellCount = (cellCount % 2 == 0) ? cellCount / 2 : cellCount / 2 + 1;
			var oddCellCount = cellCount - evenCellCount;

			var random = new Random();

			// Generate obstacles in the odd cells - odd row and column index (so that they don't intersect with each other and leave enough space between them for the tanks to pass) 
			for (int i = 0; i < oddCellCount; i++)
			{
				for (int j = 0; j < oddCellCount; j++)
				{
					if (random.Next(100) < obstacleProbability)
					{
						var center = new Vector2((i * 2 + 1.5f) * cellSize, (j * 2 + 1.5f) * cellSize); // center of the odd cell
						var radius = tankRadius + random.NextDouble() * (cellSize / 2 - tankRadius); // random size between the tank size and the cell size
						Obstacles.Add(new Obstacle(center, (float)radius));
					}
				}
			}

			// Set the start positions at random in the center of even cells (so they don't overlap with the obstacles)
			int row, column;

			row = random.Next(evenCellCount / 2) * 2; // set StartPosition1 in the top half of the map
			column = random.Next(evenCellCount) * 2;
			StartPosition1 = new Vector2((column + 0.5f) * cellSize, (row + 0.5f) * cellSize);

			row = random.Next(evenCellCount / 2, evenCellCount) * 2; // set StartPosition2 in the bottom half of the map
			column = random.Next(evenCellCount) * 2;
			StartPosition2 = new Vector2((column + 0.5f) * cellSize, (row + 0.5f) * cellSize);
		}
	}
}
